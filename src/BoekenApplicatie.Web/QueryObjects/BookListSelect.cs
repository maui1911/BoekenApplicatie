using System.Linq;
using BoekenApplicatie.Domain.Models;
using BoekenApplicatie.Web.ViewModels;

namespace BoekenApplicatie.Web.QueryObjects
{
  public static class BookListSelect
  {
    public static IQueryable<BooksViewModel> MapToViewModel(this IQueryable<Title> titles)
    {
      return titles.Select(t => new BooksViewModel
      {
        TitleId = t.Id,
        Artist = t.Book.Artist,
        Author = t.Author,
        Category = t.Category,
        Edition = t.Book.Edition,
        Genre = t.Genre,
        Translator = t.Translator,
        Publisher = t.Book.Publisher,
        Language = t.Book.Language,
        Isbn = t.Book.Isbn,
        OriginalLangage = t.OriginalLangage,
        OriginalReleasedYear = t.OriginalReleasedYear,
        OriginalTitle = t.OriginalTitle,
        PriceBought = t.Book.PriceBought,
        PriceReason = t.Book.PriceReason,
        Part = t.Part,
        Price = t.Book.Price,
        ReleasedYear = t.Book.ReleasedYear,
        TitleName = t.TitleName,
        SerialNumber = t.Book.SerialNumber,
        SeriesName = t.SeriesName,
        YearBought = t.Book.YearBought
      });
    }
  }
}