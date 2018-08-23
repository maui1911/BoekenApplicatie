using System.ComponentModel.DataAnnotations;

namespace BoekenApplicatie.Web.ViewModels
{
    public class ForgotPasswordViewModel
    {
    [Required]
    [EmailAddress]
    public string Email { get; set; }
  }
}
