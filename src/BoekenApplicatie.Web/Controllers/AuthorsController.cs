using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BoekenApplicatie.Data.Context;
using BoekenApplicatie.Domain.Models;
using BoekenApplicatie.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using BoekenApplicatie.Data.Extensions;

namespace BoekenApplicatie.Web.Controllers
{
  public class AuthorsController : Controller
  {
    private readonly LibraryContext _context;

    public AuthorsController(LibraryContext context)
    {
      _context = context;
    }

    // GET: Authors
    public async Task<IActionResult> Index(AuthorListViewModel vm)
    {
      ModelState.Remove("SortFilterPageData.PrevStateCheck");
      var options = vm.SortFilterPageData;

      if(string.IsNullOrWhiteSpace(vm.SortFilterPageData.OrderByProp))
        options.OrderByProp = "Id";

      var authorQuery = _context.Authors
        .AsNoTracking()
        .OrderBy(options.OrderByProp, options.OrderByDesc)
        .FilterBy(options.FilterByProp, options.FilterValue);

      options.SetupRestOfDto(authorQuery);
      vm.Authors = await authorQuery.Page(options.CurrentPage - 1, options.PageSize).ToListAsync();
    
      return View(vm);
    }

    // GET: Authors/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var author = await _context.Authors
        .FirstOrDefaultAsync(m => m.Id == id);
      if (author == null)
      {
        return NotFound();
      }

      return View(author);
    }

    [Authorize(Roles = "Admin")]
    // GET: Authors/Create
    public IActionResult Create()
    {
      return View();
    }

    // POST: Authors/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,LastName,FirstName,Prefix")] Author author)
    {
      if (ModelState.IsValid)
      {
        author.Id = Guid.NewGuid();
        _context.Add(author);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
      }

      return View(author);
    }

    // GET: Authors/Edit/5
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(Guid? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var author = await _context.Authors.FindAsync(id);
      if (author == null)
      {
        return NotFound();
      }

      return View(author);
    }

    // POST: Authors/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("Id,LastName,FirstName,Prefix")] Author author)
    {
      if (id != author.Id)
      {
        return NotFound();
      }

      if (ModelState.IsValid)
      {
        try
        {
          _context.Update(author);
          await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
          if (!AuthorExists(author.Id))
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

      return View(author);
    }

    [Authorize(Roles = "Admin")]
    // GET: Authors/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var author = await _context.Authors
        .FirstOrDefaultAsync(m => m.Id == id);
      if (author == null)
      {
        return NotFound();
      }

      return View(author);
    }

    // POST: Authors/Delete/5
    [Authorize(Roles = "Admin")]
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
      var author = await _context.Authors.FindAsync(id);
      _context.Authors.Remove(author);
      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }

    private bool AuthorExists(Guid id)
    {
      return _context.Authors.Any(e => e.Id == id);
    }
  }
}