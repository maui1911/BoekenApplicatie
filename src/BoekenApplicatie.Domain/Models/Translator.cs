using System;
using System.Collections.Generic;
using System.Text;

namespace BoekenApplicatie.Domain.Models
{
  public class Translator : Person, IModel
  {
    public Translator()
    {
      Books = new HashSet<Book>();
    }
    public ICollection<Book> Books { get; set; }
  }

}
