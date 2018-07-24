using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using BoekenApplicatie.Domain.Models;
using BoekenApplicatie.Web.Areas.Identity.Pages.Account;
using BoekenApplicatie.Web.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BoekenApplicatie.Web.Controllers
{
  public class AccountController : Controller
  {
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager; 
    private readonly IEmailSender _emailSender;

    public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IEmailSender emailSender)
    {
      _signInManager = signInManager;
      _userManager = userManager;
      _emailSender = emailSender;
    }

    public IActionResult Index()
    {
      return View();
    }

    public IActionResult Edit()
    {
      return View();
    }

    public async Task<IActionResult> Edit(ApplicationUser user)
    {
      return View();
    }

    [HttpPost]
    public async Task<IActionResult> Logout(string returnUrl = null)
    {
      await _signInManager.SignOutAsync();
      return returnUrl != null ? LocalRedirect(returnUrl) : Index();
    }

    [HttpGet]
    public IActionResult Register(string returnUrl = null)
    {
      return View(new RegisterViewModel { ReturnUrl = returnUrl });
    }
    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel vm)
    {
      vm.ReturnUrl = vm.ReturnUrl ?? Url.Content("~/");
      if (!ModelState.IsValid) return View(vm);

      var user = new ApplicationUser { UserName = vm.Email, Email = vm.Email, FirstName = vm.FirstName, LastName = vm.LastName};
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
    public async Task<IActionResult> Login(string returnUrl = null)
    {
      var viewModel = new LoginViewModel();
      
      returnUrl = returnUrl ?? Url.Content("~/");

      // Clear the existing external cookie to ensure a clean login process
      await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

      viewModel.ReturnUrl = returnUrl;

      return View(viewModel);
    }
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel vm)
    {
      vm.ReturnUrl = vm.ReturnUrl ?? Url.Content("~/");

      if (ModelState.IsValid)
      {
        // This doesn't count login failures towards account lockout
        // To enable password failures to trigger account lockout, set lockoutOnFailure: true
        var result = await _signInManager.PasswordSignInAsync(vm.Input.Email, vm.Input.Password, vm.Input.RememberMe, lockoutOnFailure: false);
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