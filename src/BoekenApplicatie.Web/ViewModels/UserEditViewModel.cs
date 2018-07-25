using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BoekenApplicatie.Web.ViewModels
{
  public class UserEditViewModel
  {
    public Guid Id { get; set; }
    [DisplayName("Achternaam")] public string LastName { get; set; }
    [DisplayName("Voornaam")] public string FirstName { get; set; }
    [DisplayName("Tussenvoegsel")] public string Prefix { get; set; }
    [DisplayName("Adres")] public string Address { get; set; }
    [DisplayName("Postcode")] public string ZipCode { get; set; }
    [DisplayName("Plaats")] public string Residence { get; set; }
    [DisplayName("Telefoonnummer")] public string PhoneNumber { get; set; }
    [TempData] public string ErrorMessage { get; set; }
    public List<SelectListItem> RolesList { get; set; } = new List<SelectListItem>();
    } 
}