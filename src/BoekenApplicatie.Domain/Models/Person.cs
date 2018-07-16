using System;
using System.Collections.Generic;
using System.Text;
//using Microsoft.AspNet.Identity.EntityFramework;
//using Microsoft.AspNetCore.Identity;

namespace BoekenApplicatie.Domain.Models
{
  public abstract class Person : IModel
  {
    public Guid Id { get; set; }
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string Prefix { get; set; }
    public string Address { get; set; }
    public string ZipCode { get; set; }
    public string Residence { get; set; }
    public PersonType Type { get; set; }
  }
}
