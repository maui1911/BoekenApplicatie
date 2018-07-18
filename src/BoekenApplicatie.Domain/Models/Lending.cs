using System;
using System.Collections.Generic;
using System.Text;

namespace BoekenApplicatie.Domain.Models
{
  public class Lending : IModel
  {
    public Guid Id { get; set; }
    public Book Book { get; set; }
    public Lender Lender { get; set; }
    public DateTimeOffset? StartLend { get; set; }
    public DateTimeOffset? EndLend { get; set; }
  }
}
