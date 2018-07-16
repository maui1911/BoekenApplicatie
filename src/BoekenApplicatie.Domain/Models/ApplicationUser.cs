using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace BoekenApplicatie.Domain.Models
{
  public class ApplicationUser : IdentityUser<Guid>, IModel
  {
    public ApplicationUser()
    {
      Lendings = new HashSet<Lending>();
      Ratings = new HashSet<Rating>();
    }
    public override Guid Id { get; set; }
    public Person Lender { get; set; }
    public ICollection<Lending> Lendings { get; set; }
    public ICollection<Rating> Ratings { get; set; }
  }
}
