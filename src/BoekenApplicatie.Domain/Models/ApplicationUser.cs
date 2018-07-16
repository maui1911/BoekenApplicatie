using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace BoekenApplicatie.Domain.Models
{
  public class ApplicationUser : IdentityUser<Guid>, IModel
  {
    public override Guid Id { get; set; }
    public Lender Lender { get; set; }
  }
}
