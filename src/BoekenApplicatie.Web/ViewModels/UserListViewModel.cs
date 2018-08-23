using System.Collections.Generic;
using BoekenApplicatie.Domain.Models;

namespace BoekenApplicatie.Web.ViewModels
{
    public class UserListViewModel : BaseListViewModel
    {
      public UserListViewModel()
      {
        ApplicationUsers = new List<ApplicationUser>();
      }
      public List<ApplicationUser> ApplicationUsers { get; set; }
  }
}
