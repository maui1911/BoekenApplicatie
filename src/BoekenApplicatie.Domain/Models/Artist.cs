using System;
using System.Collections.Generic;
using System.Text;

namespace BoekenApplicatie.Domain.Models
{
  public class Artist : Person, IModel
  {
    public virtual ICollection<Book> Books { get; set; }
  }
}
