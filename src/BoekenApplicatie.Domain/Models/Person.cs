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

    [DisplayName("Naam")]
    public string Name { get
      {
        if(string.IsNullOrWhiteSpace(Prefix))
        {
          return $"{FirstName} {LastName}";
        }
        return $"{FirstName} {Prefix} {LastName}";
      }
    }

  }
}
