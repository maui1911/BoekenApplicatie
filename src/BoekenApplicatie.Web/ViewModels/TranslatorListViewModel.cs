using System.Collections.Generic;
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
