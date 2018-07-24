using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BoekenApplicatie.Web.Areas.Identity.Pages.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace BoekenApplicatie.Web.ViewModels
{
    public class LoginViewModel
    {
      [BindProperty]
      public LoginModel.InputModel Input { get; set; }

      public IList<AuthenticationScheme> ExternalLogins { get; set; }

      public string ReturnUrl { get; set; }

      [TempData]
      public string ErrorMessage { get; set; }

      public class InputModel
      {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
      }
  }
}
