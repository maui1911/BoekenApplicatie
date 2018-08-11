using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoekenApplicatie.Domain.Models;

namespace BoekenApplicatie.Web.ViewModels
{
    public class TranslatorListViewModel : BaseListViewModel
    {
      public TranslatorListViewModel()
      {
        Translators = new List<Translator>();
      }
      public List<Translator> Translators { get; set; }
  }
}
