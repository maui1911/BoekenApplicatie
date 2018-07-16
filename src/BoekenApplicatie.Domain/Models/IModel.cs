using System;
using System.Collections.Generic;
using System.Text;

namespace BoekenApplicatie.Domain.Models
{
  public interface IModel
  {
    Guid Id { get; set; }
  }
}
