using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;
using System.Text;

namespace BoekenApplicatie.Domain.Models
{
  public class Title : IModel
  {
    public Guid Id { get; set; }
    public string TitleName { get; set; }
    public string SeriesName { get; set; }
    public virtual Author Author { get; set; }
    public int Part { get; set; }
    public Category Category { get; set; }
    public Genre Genre { get; set; }
    public string OriginalTitle { get; set; }
    public Language OriginalLangage { get; set; }
    public int OriginalReleasedYear { get; set; }
    public virtual Translator Translator { get; set; }
    public virtual Book Book { get; set; }
  }
}
