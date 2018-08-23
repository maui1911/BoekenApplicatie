using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace BoekenApplicatie.Web.ViewModels
{
    public class LoginViewModel
    {
      public InputModel Input { get; set; }

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
