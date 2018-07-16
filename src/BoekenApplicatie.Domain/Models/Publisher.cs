using System;
using System.Collections.Generic;
using System.Text;

namespace BoekenApplicatie.Domain.Models
{
  public class Publisher : IModel
  {
    public Publisher()
    {
      Books = new HashSet<Book>();
    }
    public Guid Id { get; set; }
    public string Name { get; set; }
    public ICollection<Book> Books { get; set; }
  }
}
