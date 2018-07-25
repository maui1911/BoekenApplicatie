using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace BoekenApplicatie.Web.Configuration
{
  public class RoleProfile
  {
    public static void CreateUserRoles(IServiceProvider serviceProvider)
    {
      var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

      //Adding Admin Role
      var roleCheck = roleManager.RoleExistsAsync("Admin");
      roleCheck.Wait();
      if (!roleCheck.Result)
      {
        //create the roles and seed them to the database
        var result = roleManager.CreateAsync(new IdentityRole<Guid>("Admin"));
        result.Wait();
      }

      //Adding Lender role Role
      roleCheck = roleManager.RoleExistsAsync("Lender");
      roleCheck.Wait();
      if (!roleCheck.Result)
      {
        var result = roleManager.CreateAsync(new IdentityRole<Guid>("Lender"));
        result.Wait();
      }
    }
  }
}