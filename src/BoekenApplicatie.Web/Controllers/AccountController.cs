﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BoekenApplicatie.Web.Controllers
{
  public class AccountController : Controller
  {
    public IActionResult Index()
    {
      return View();
    }

    public IActionResult Edit()
    {
      return View();
    }
  }
}