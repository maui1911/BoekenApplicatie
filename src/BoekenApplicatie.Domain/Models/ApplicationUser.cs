using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace BoekenApplicatie.Domain.Models
{
  public class ApplicationUser : IdentityUser<Guid>, IModel
  {
    public override Guid Id { get; set; }
    [DisplayName("Achternaam")] public string LastName { get; set; }
    [DisplayName("Voornaam")] public string FirstName { get; set; }
    [DisplayName("Tussenvoegsel")] public string Prefix { get; set; }
    [DisplayName("Adres")] public string Address { get; set; }
    [DisplayName("Postcode")] public string ZipCode { get; set; }
    [DisplayName("Plaats")] public string Residence { get; set; }

    public virtual ICollection<Rating> Ratings { get; set; }
    public virtual ICollection<Lending> Lendings { get; set; }

    [DisplayName("Naam")]
    public string Name => string.IsNullOrWhiteSpace(Prefix)
      ? $"{FirstName} {LastName}"
      : $"{FirstName} {Prefix} {LastName}";
  }
}