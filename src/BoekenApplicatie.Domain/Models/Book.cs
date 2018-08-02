using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BoekenApplicatie.Domain.Models
{
  public class Book : IModel
  {
    public Book()
    {
      Titles = new HashSet<Title>();
      Ratings = new HashSet<Rating>();
      Lendings = new HashSet<Lending>();
      Language = Language.Nederlands;

    }
    public Guid Id { get; set; }
    public ICollection<Title> Titles { get; set; }
    public Publisher Publisher { get; set; }
    public string Isbn { get; set; }
    public string SerialNumber { get; set; }
    public int ReleasedYear { get; set; }
    public int Edition { get; set; }
    [DataType(DataType.Currency)]
    public decimal Price { get; set; }
    public Language Language { get; set; }
    public Artist Artist { get; set; }
    public int YearBought { get; set; }
    [DataType(DataType.Currency)]
    public decimal PriceBought { get; set; }
    public string PriceReason { get; set; }
    public ICollection<Rating> Ratings { get; set; }
    public ICollection<Lending> Lendings { get; set; }
  }
}
