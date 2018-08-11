using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoekenApplicatie.Web.ViewModels
{
    public class BookListViewModel : BaseListViewModel
    {
      public BookListViewModel()
      {
        BooksViewModels = new List<BooksViewModel>();
      }
      public List<BooksViewModel> BooksViewModels { get; set; }
  }
}
