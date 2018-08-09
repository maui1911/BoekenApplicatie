using System;
using System.Collections.Generic;
using System.Text;

namespace BoekenApplicatie.Domain.Models
{
  public class Author : Person, IModel
  {

    public virtual ICollection<Title> Titles { get; set; }
  }
}
