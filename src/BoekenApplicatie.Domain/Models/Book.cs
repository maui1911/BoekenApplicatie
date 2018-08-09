using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BoekenApplicatie.Domain.Models
{
  public class Book : IModel
  {
 
    public Guid Id { get; set; }
    public virtual ICollection<Title> Titles { get; set; }
    public virtual Publisher Publisher { get; set; }
    public string Isbn { get; set; }
    public string SerialNumber { get; set; }
    public int ReleasedYear { get; set; }
    public int Edition { get; set; }
    [DataType(DataType.Currency)]
    public decimal Price { get; set; }
    public Language Language { get; set; }
    public virtual Artist Artist { get; set; }
    public int YearBought { get; set; }
    [DataType(DataType.Currency)]
    public decimal PriceBought { get; set; }
    public string PriceReason { get; set; }
    public virtual ICollection<Rating> Ratings { get; set; }
    public virtual ICollection<Lending> Lendings { get; set; }
  }
}
