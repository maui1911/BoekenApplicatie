﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace BoekenApplicatie.Domain.Models
{
  public class ApplicationUser : IdentityUser<Guid>, IModel
  {
    public ApplicationUser()
    {
      Ratings = new HashSet<Rating>();
      Lendings = new HashSet<Lending>();
    }

    public override Guid Id { get; set; }
    [DisplayName("Achternaam")]
    public string LastName { get; set; }
    [DisplayName("Voornaam")]
    public string FirstName { get; set; }
    [DisplayName("Tussenvoegsel")]
    public string Prefix { get; set; }
    [DisplayName("Adres")]
    public string Address { get; set; }
    [DisplayName("Postcode")]
    public string ZipCode { get; set; }
    [DisplayName("Plaats")]
    public string Residence { get; set; }

    public ICollection<Rating> Ratings { get; set; }
    public ICollection<Lending> Lendings { get; set; }
  }
}
