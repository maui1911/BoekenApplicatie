using System.Collections.Generic;

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