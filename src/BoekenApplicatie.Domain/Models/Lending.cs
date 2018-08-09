using System;
using System.Collections.Generic;
using System.Text;

namespace BoekenApplicatie.Domain.Models
{
  public class Lending : IModel
  {
    public Guid Id { get; set; }
    public virtual Book Book { get; set; }
    public virtual ApplicationUser User { get; set; }
    public DateTimeOffset? StartLend { get; set; }
    public DateTimeOffset? EndLend { get; set; }
  }
}
