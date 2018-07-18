using System;
using System.Collections.Generic;
using System.Text;
//using Microsoft.AspNet.Identity.EntityFramework;
//using Microsoft.AspNetCore.Identity;
using System.ComponentModel;

namespace BoekenApplicatie.Domain.Models
{
  public abstract class Person : IModel
  {
    public Guid Id { get; set; }

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
   // public PersonType Type { get; set; }
  }
}
