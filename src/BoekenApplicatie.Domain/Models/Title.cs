using System;
using System.Collections.Generic;
using System.Text;

namespace BoekenApplicatie.Domain.Models
{
  public class Title : IModel
  {
    public Guid Id { get; set; }
    public string TitleName { get; set; }
    public string SeriesName { get; set; }
    public Author Author { get; set; }
    public int Part { get; set; }
    public Category Category { get; set; }
    public string OriginalTitle { get; set; }
    public Language OriginalLangage { get; set; }
    public int OriginalReleasedYear { get; set; }
    public Translator Translator { get; set; }
  }
}
