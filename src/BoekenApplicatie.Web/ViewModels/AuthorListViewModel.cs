using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoekenApplicatie.Domain.Models;

namespace BoekenApplicatie.Web.ViewModels
{
  public class AuthorListViewModel : BaseListViewModel
  {
    public AuthorListViewModel()
    {
      Authors = new List<Author>();
    }
    public List<Author> Authors { get; set; }
  }
}
