﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BoekenApplicatie.Web.Areas.Identity.Pages.Account;
using Microsoft.AspNetCore.Mvc;

namespace BoekenApplicatie.Web.ViewModels
{
  public class RegisterViewModel
  {
    public string ReturnUrl { get; set; }

    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    public string Email { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",
      MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }

    [Required]
    [Display(Name = "Voornaam")]
    public string FirstName { get; set; }

    [Required]
    [Display(Name = "Achternaam")]
    public string LastName { get; set; }
  }
}