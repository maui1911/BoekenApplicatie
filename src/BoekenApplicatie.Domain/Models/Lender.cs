using System;
using System.Collections.Generic;
using System.Text;

namespace BoekenApplicatie.Domain.Models
{
  public class Lender : Person, IModel
  {
    public Lender()
    {
      Lendings = new HashSet<Lending>();
      Ratings = new HashSet<Rating>();
    }
    public ApplicationUser User { get; set; }
    public ICollection<Lending> Lendings { get; set; }
    public ICollection<Rating> Ratings { get; set; }
  }
}
