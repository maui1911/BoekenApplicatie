﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BoekenApplicatie.Domain.Models
{
  public class Rating : IModel
  {
    public Guid Id { get; set; }
    public double Rate { get; set; }
    public string Remarks { get; set; }
    public virtual Book Book { get; set; }
    public virtual ApplicationUser User { get; set; }
    public DateTimeOffset? RatingDate { get; set; }
  }
}
