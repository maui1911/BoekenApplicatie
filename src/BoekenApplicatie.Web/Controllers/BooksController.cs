using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoekenApplicatie.Web.ViewModels;
using BoekenApplicatie.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BoekenApplicatie.Data.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace BoekenApplicatie.Web.Controllers
{
  public class BooksController : Controller
  {
    private readonly LibraryContext _context;
    public BooksController(LibraryContext context)
    {
      _context = context;
    }
    // GET: Books
    public async Task<ActionResult> Index()
    {

      var books = await _context.Books
        .Include(book => book.Titles)
          .ThenInclude(title => title.Author)
        .ToListAsync();

      List<BooksViewModel> booksViewModels = new List<BooksViewModel>();
      booksViewModels.AddRange(books.Select(CreateBookViewModelForIndex));

     // foreach(var book in books)
     // {        
     //   booksViewModels.Add(CreateBookViewModel(book));
      //}

      return View(booksViewModels);
    }

    private static BooksViewModel CreateBookViewModelForIndex(Book book)
    {
      return new BooksViewModel
      {
        BookId = book.Id,
        TitleName = book.Titles.FirstOrDefault()?.TitleName,
        SeriesName = book.Titles.FirstOrDefault()?.SeriesName,
        Author = book.Titles.FirstOrDefault()?.Author,
        Language = book.Language,
        Publisher = book.Publisher,
        SerialNumber = book.SerialNumber,
        Isbn = book.Isbn,
        ReleasedYear = book.ReleasedYear,
        Price = book.Price
      };
    }

    // GET: Books/Details/5
    public ActionResult Details(int id)
    {
      return View();
    }
    [Authorize(Roles = "Admin")]
    // GET: Books/Create
    public async Task<ActionResult> Create()
    {
      var booksViewModel = new BooksViewModel();

      var authors = await _context.Authors.ToListAsync();

      booksViewModel.Authors.AddRange(authors.Select(author => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = author.Id.ToString(), Text = author.Name }));
      return View(booksViewModel);
    }

    // POST: Books/Create
    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(BooksViewModel viewModel)
    {
      if (!ModelState.IsValid) return View(viewModel);
      var title = new Title
      {
        Id = Guid.NewGuid(),
        Author = await _context.Authors.FindAsync(Guid.Parse(viewModel.AuthorId)),
        Category = viewModel.Category,
        OriginalReleasedYear = viewModel.OriginalReleasedYear,
        OriginalLangage = viewModel.OriginalLangage,
        OriginalTitle = viewModel.OriginalTitle,
        Part = viewModel.Part,
        SeriesName = viewModel.SeriesName,
        TitleName = viewModel.TitleName,
        Translator = viewModel.Translator
      };

      var book = new Book
      {
        Id = Guid.NewGuid(),
        Artist = viewModel.Artist,
        Edition = viewModel.Edition,
        Isbn = viewModel.Isbn,
        Language = viewModel.Language,
        Price = viewModel.Price,
        PriceBought = viewModel.PriceBought,
        PriceReason = viewModel.PriceReason,
        Publisher = viewModel.Publisher,
        ReleasedYear = viewModel.ReleasedYear,
        YearBought = viewModel.YearBought
      };

      book.Titles.Add(title);

      _context.Add(book);
      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));

    }

    // GET: Books/Edit/5
    [Authorize(Roles = "Admin")]
    public ActionResult Edit(int id)
    {
      return View();
    }

    // POST: Books/Edit/5
    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(int id, IFormCollection collection)
    {
      try
      {
        // TODO: Add update logic here

        return RedirectToAction(nameof(Index));
      }
      catch
      {
        return View();
      }
    }

    // GET: Books/Delete/5
    [Authorize(Roles = "Admin")]
    public ActionResult Delete(int id)
    {
      return View();
    }

    // POST: Books/Delete/5
    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Delete(int id, IFormCollection collection)
    {
      try
      {
        // TODO: Add delete logic here

        return RedirectToAction(nameof(Index));
      }
      catch
      {
        return View();
      }
    }
  }
}