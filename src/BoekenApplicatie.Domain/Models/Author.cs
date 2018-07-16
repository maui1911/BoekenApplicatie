using System;
using System.Collections.Generic;
using System.Text;

namespace BoekenApplicatie.Domain.Models
{
  public class Author : Person, IModel
  {
    public Author()
    {
      Titles = new HashSet<Title>();
    }
    public ICollection<Title> Titles { get; set; }
  }
}
