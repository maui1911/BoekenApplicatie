using System.Collections.Generic;
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
