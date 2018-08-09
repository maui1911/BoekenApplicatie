﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
    private readonly IMapper _mapper;
    public BooksController(LibraryContext context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }
    // GET: Books
    public async Task<ActionResult> Index()
    {
      var titles = await _context.Titles.ToListAsync();
      
      List<BooksViewModel> booksViewModels = new List<BooksViewModel>();
      booksViewModels.AddRange(titles.Select(CreateBookViewModelForIndex));
      
      return View(booksViewModels);
    }

    private BooksViewModel CreateBookViewModelForIndex(Title title)
    {
      var bookViewModel = _mapper.Map<Title,BooksViewModel>(title);
      _mapper.Map(title.Book, bookViewModel);
      bookViewModel.TitleId = title.Id;
      return bookViewModel;
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
      await GetDropdownData(booksViewModel);

      return View(booksViewModel);
    }

    private async Task GetDropdownData(BooksViewModel booksViewModel)
    {
      var authors = await _context.Authors.ToListAsync();
      var publishers = await _context.Publishers.ToListAsync();
      var translators = await _context.Translators.ToListAsync();
      var artists = await _context.Artists.ToListAsync();

      booksViewModel.Authors.AddRange(authors.Select(author => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = author.Id.ToString(), Text = author.Name }));
      booksViewModel.Publishers.AddRange(publishers.Select(publisher => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = publisher.Id.ToString(), Text = publisher.Name }));
      booksViewModel.Translators.AddRange(translators.Select(translator => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = translator.Id.ToString(), Text = translator.Name }));
      booksViewModel.Artists.AddRange(artists.Select(artist => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = artist.Id.ToString(), Text = artist.Name }));
    }

    // POST: Books/Create
    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(BooksViewModel viewModel)
    {
      if (!ModelState.IsValid) return View(viewModel);
      
      var title = _mapper.Map<BooksViewModel, Title>(viewModel);
      title.Id = Guid.NewGuid();
      title.Author = await _context.Authors.FindAsync(Guid.Parse(viewModel.AuthorId));
      title.Translator = await _context.Translators.FindAsync(Guid.Parse(viewModel.TranslatorId));


      var book = _mapper.Map<BooksViewModel, Book>(viewModel);
      book.Id = Guid.NewGuid();
      book.Publisher = await _context.Publishers.FindAsync(Guid.Parse(viewModel.PublisherId));
      book.Artist = await _context.Artists.FindAsync(Guid.Parse(viewModel.ArtistId));
      book.Titles.Add(title);

      _context.Add(book);
      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }

    // GET: Books/Edit/5
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> Edit(Guid? id)
    {
      if(id == null)
      {
        return NotFound();
      }

      var title = await _context.Titles.FindAsync(id);
      if(title == null)
      {
        return NotFound();
      }

      var booksViewModel = _mapper.Map<Title,BooksViewModel> (title);
      _mapper.Map<Book, BooksViewModel>(title.Book, booksViewModel);
      booksViewModel.TitleId = title.Id;
      await GetDropdownData(booksViewModel);

      return View(booksViewModel);
    }

    // POST: Books/Edit/5
    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(BooksViewModel viewModel)
    {
      try
      {
        var title = await _context.Titles.FindAsync(viewModel.TitleId);
        _mapper.Map(viewModel, title);
        title.Author = await _context.Authors.FindAsync(Guid.Parse(viewModel.AuthorId));
        title.Translator = await _context.Translators.FindAsync(Guid.Parse(viewModel.TranslatorId));

        _mapper.Map(viewModel, title.Book);
        title.Book.Publisher = await _context.Publishers.FindAsync(Guid.Parse(viewModel.PublisherId));
        title.Book.Artist = await _context.Artists.FindAsync(Guid.Parse(viewModel.ArtistId));

        await _context.SaveChangesAsync();

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