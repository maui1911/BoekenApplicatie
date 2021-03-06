﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BoekenApplicatie.Data.Context;
using BoekenApplicatie.Data.Extensions;
using BoekenApplicatie.Domain.Models;
using BoekenApplicatie.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace BoekenApplicatie.Web.Controllers
{
  public class ArtistsController : Controller
  {
    private readonly LibraryContext _context;

    public ArtistsController(LibraryContext context)
    {
      _context = context;
    }

    // GET: Artists
    public async Task<IActionResult> Index(ArtistListViewModel vm)
    {
      ModelState.Remove("SortFilterPageData.PrevStateCheck");
      var options = vm.SortFilterPageData;

      if(string.IsNullOrWhiteSpace(vm.SortFilterPageData.OrderByProp))
        options.OrderByProp = "Id";

      var authorQuery = _context.Artists
        .AsNoTracking()
        .OrderBy(options.OrderByProp, options.OrderByDesc)
        .FilterBy(options.FilterByProp, options.FilterValue);

      options.SetupRestOfDto(authorQuery);
      vm.Artists = await authorQuery.Page(options.CurrentPage - 1, options.PageSize).ToListAsync();

      return View(vm);
    }


    // GET: Artists/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var artist = await _context.Artists
        .FirstOrDefaultAsync(m => m.Id == id);
      if (artist == null)
      {
        return NotFound();
      }

      return View(artist);
    }

    [Authorize(Roles = "Admin")]
    // GET: Artists/Create
    public IActionResult Create()
    {
      return View();
    }

    // POST: Artists/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,LastName,FirstName,Prefix")] Artist artist)
    {
      if (ModelState.IsValid)
      {
        artist.Id = Guid.NewGuid();
        _context.Add(artist);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
      }

      return View(artist);
    }

    [Authorize(Roles = "Admin")]
    // GET: Artists/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var artist = await _context.Artists.FindAsync(id);
      if (artist == null)
      {
        return NotFound();
      }

      return View(artist);
    }

    [Authorize(Roles = "Admin")]
    // POST: Artists/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("Id,LastName,FirstName,Prefix")] Artist artist)
    {
      if (id != artist.Id)
      {
        return NotFound();
      }

      if (ModelState.IsValid)
      {
        try
        {
          _context.Update(artist);
          await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
          if (!ArtistExists(artist.Id))
          {
            return NotFound();
          }
          else
          {
            throw;
          }
        }

        return RedirectToAction(nameof(Index));
      }

      return View(artist);
    }

    [Authorize(Roles = "Admin")]
    // GET: Artists/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var artist = await _context.Artists
        .FirstOrDefaultAsync(m => m.Id == id);
      if (artist == null)
      {
        return NotFound();
      }

      return View(artist);
    }

    [Authorize(Roles = "Admin")]
    // POST: Artists/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
      var artist = await _context.Artists.FindAsync(id);
      _context.Artists.Remove(artist);
      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }

    private bool ArtistExists(Guid id)
    {
      return _context.Artists.Any(e => e.Id == id);
    }
  }
}