﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoekenApplicatie.Domain.Models;

namespace BoekenApplicatie.Web.ViewModels
{
  public class ArtistListViewModel : BaseListViewModel
  {
    public ArtistListViewModel()
    {
      Artists = new List<Artist>();
    }

    public List<Artist> Artists { get; set; }
  }
}
