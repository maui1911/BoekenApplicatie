using System;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using AutoMapper;
using BoekenApplicatie.Domain.Models;
using BoekenApplicatie.Web.Extensions;
using BoekenApplicatie.Web.Services;
using BoekenApplicatie.Web.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using BoekenApplicatie.Data.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BoekenApplicatie.Web.Controllers
{
  public class AccountController : Controller
  {
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;
    private readonly IMapper _mapper;
    private readonly IMailservice _mailservice;
    
    public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager,
      IMapper mapper, RoleManager<IdentityRole<Guid>> roleManager, IMailservice mailservice)
    {
      _signInManager = signInManager;
      _userManager = userManager;
      _mapper = mapper;
      _roleManager = roleManager;
      _mailservice = mailservice;
    }

    [Authorize]
    public async Task<IActionResult> Index(UserListViewModel vm)
    {
      ModelState.Remove("SortFilterPageData.PrevStateCheck");
      var options = vm.SortFilterPageData;

      if(string.IsNullOrWhiteSpace(vm.SortFilterPageData.OrderByProp))
        options.OrderByProp = "Id";

      var userQuery = _userManager.Users
        .AsNoTracking()
        .OrderBy(options.OrderByProp, options.OrderByDesc)
        .FilterBy(options.FilterByProp, options.FilterValue);

      options.SetupRestOfDto(userQuery);
      vm.ApplicationUsers = await userQuery.Page(options.CurrentPage - 1, options.PageSize).ToListAsync();

      return View(vm);
    }

    [Authorize]
    public async Task<IActionResult> Edit(string id = null)
    {
      var isAdmin = await _userManager.IsInRoleAsync(await _userManager.GetUserAsync(User), "Admin");

      if (string.IsNullOrWhiteSpace(id) || !isAdmin)
      {
        id = _userManager.GetUserId(User);
      }

      var user = await _userManager.FindByIdAsync(id);
      var vm = _mapper.Map<ApplicationUser, UserEditViewModel>(user);

      var roles = _roleManager.Roles;
      foreach (var role in roles)
      {
        var roleListItem = new SelectListItem
        {
          Text = role.Name,
          Value = role.Id.ToString(),
          Selected = await _userManager.IsInRoleAsync(user, role.Name)
        };
        vm.RolesList.Add(roleListItem);
      }

      return View(vm);
    }

    [Authorize]
    [ValidateAntiForgeryToken]
    [HttpPost]
    public async Task<IActionResult> Edit(UserEditViewModel vm)
    {
      if (!ModelState.IsValid) return View(vm);

      var user = await _userManager.FindByIdAsync(vm.Id.ToString());
      _mapper.Map(vm, user);

      foreach (var role in vm.RolesList)
      {
        if (role.Selected)
        {
          await _userManager.AddToRoleAsync(user, role.Text);
        }
        else
        {
          if (await _userManager.IsInRoleAsync(user, role.Text))
          {
            await _userManager.RemoveFromRoleAsync(user, role.Text);
          }
        }
      }

      var result = await _userManager.UpdateAsync(user);

      if (result.Succeeded)
      {
        if (User.Identity.Name == user.UserName)
        {
          await HttpContext.RefreshLoginAsync();
        }

        return RedirectToAction("Index");
      }

      foreach (var error in result.Errors)
      {
        ModelState.AddModelError(string.Empty, error.Description);
      }

      return View(vm);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Logout(string returnUrl = null)
    {
      await _signInManager.SignOutAsync();
      return returnUrl != null ? (IActionResult) LocalRedirect(returnUrl) : RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult Register(string returnUrl = null)
    {
      return View(new RegisterViewModel {ReturnUrl = returnUrl});
    }

    [ValidateAntiForgeryToken]
    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel vm)
    {
      vm.ReturnUrl = vm.ReturnUrl ?? Url.Content("~/");
      if (!ModelState.IsValid) return View(vm);

      var user = new ApplicationUser
      {
        UserName = vm.Email,
        Email = vm.Email,
        FirstName = vm.FirstName,
        LastName = vm.LastName
      };
      var result = await _userManager.CreateAsync(user, vm.Password);
      if (result.Succeeded)
      {
        await _signInManager.SignInAsync(user, isPersistent: false);
        return LocalRedirect(vm.ReturnUrl);
      }

      foreach (var error in result.Errors)
      {
        ModelState.AddModelError(string.Empty, error.Description);
      }

      return View(vm);
    }

    [HttpGet]
    public IActionResult AccessDenied()
    {
      return View();
    }

    public IActionResult ForgotPassword()
    {
      return View(new ForgotPasswordViewModel()); 
    }

    [ValidateAntiForgeryToken]
    [HttpPost]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel vm)
    {
      if (!ModelState.IsValid) return View(vm);

      var user = await _userManager.FindByEmailAsync(vm.Email);
      if (user == null )//|| !(await _userManager.IsEmailConfirmedAsync(user)))
      {
        // Don't reveal that the user does not exist or is not confirmed
        return RedirectToAction("ForgotPasswordConfirmation");
      }

      // For more information on how to enable account confirmation and password reset please 
      // visit https://go.microsoft.com/fwlink/?LinkID=532713
      var code = await _userManager.GeneratePasswordResetTokenAsync(user);
      var callbackUrl = Url.Action("ResetPassword", "Account",
        values: new { code },
        protocol: Request.Scheme);

      await _mailservice.SendMailAsync(
        vm.Email,
        "Reset Password",
        $"Please reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

      return RedirectToAction("ForgotPasswordConfirmation");
    }

    [HttpGet]
    public IActionResult ForgotPasswordConfirmation()
    {
      return View();
    }

    [HttpGet]
    public IActionResult ResetPassword(string code = null)
    {
      if (code == null)
      {
        return BadRequest("A code must be supplied for password reset.");
      }

      return View(new ResetPasswordViewModel {Code = code});
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ResetPassword(ResetPasswordViewModel vm)
    {
      if (!ModelState.IsValid)
      {
        return View(vm);
      }

      var user = await _userManager.FindByEmailAsync(vm.Email);
      if (user == null)
      {
        // Don't reveal that the user does not exist
        return RedirectToAction("ResetPasswordConfirmation");
      }

      var result = await _userManager.ResetPasswordAsync(user, vm.Code, vm.Password);
      if (result.Succeeded)
      {
        return RedirectToAction("ResetPasswordConfirmation");
      }

      foreach (var error in result.Errors)
      {
        ModelState.AddModelError(string.Empty, error.Description);
      }
      return View(vm);
    }

    [HttpGet]
    public IActionResult ResetPasswordConfirmation()
    {
      return View();
    }

    [HttpGet]
    public async Task<IActionResult> Login(string returnUrl = null)
    {
      var viewModel = new LoginViewModel();

      returnUrl = returnUrl ?? Url.Content("~/");

      // Clear the existing external cookie to ensure a clean login process
      await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

      viewModel.ReturnUrl = returnUrl;

      return View(viewModel);
    }

    [ValidateAntiForgeryToken]
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel vm)
    {
      vm.ReturnUrl = vm.ReturnUrl ?? Url.Content("~/");

      if (ModelState.IsValid)
      {
        // This doesn't count login failures towards account lockout
        // To enable password failures to trigger account lockout, set lockoutOnFailure: true
        var result = await _signInManager.PasswordSignInAsync(vm.Input.Email, vm.Input.Password, vm.Input.RememberMe,
          lockoutOnFailure: false);
        if (result.Succeeded)
        {
          return LocalRedirect(vm.ReturnUrl);
        }

        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        return View(vm);
      }

      return View(vm);
    }
  }
}