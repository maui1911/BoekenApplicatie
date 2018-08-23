using System;
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
  public class TranslatorsController : Controller
  {
    private readonly LibraryContext _context;

    public TranslatorsController(LibraryContext context)
    {
      _context = context;
    }

    // GET: Translators
    public async Task<IActionResult> Index(TranslatorListViewModel vm)
    {
      ModelState.Remove("SortFilterPageData.PrevStateCheck");
      var options = vm.SortFilterPageData;

      if(string.IsNullOrWhiteSpace(vm.SortFilterPageData.OrderByProp))
        options.OrderByProp = "Id";

      var authorQuery = _context.Translators
        .AsNoTracking()
        .OrderBy(options.OrderByProp, options.OrderByDesc)
        .FilterBy(options.FilterByProp, options.FilterValue);

      options.SetupRestOfDto(authorQuery);
      vm.Translators = await authorQuery.Page(options.CurrentPage - 1, options.PageSize).ToListAsync();

      return View(vm);
    }

    // GET: Translators/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
      if(id == null)
      {
        return NotFound();
      }

      var translator = await _context.Translators
        .FirstOrDefaultAsync(m => m.Id == id);
      if(translator == null)
      {
        return NotFound();
      }

      return View(translator);
    }

    // GET: Translators/Create
    [Authorize(Roles = "Admin")]
    public IActionResult Create()
    {
      return View();
    }

    // POST: Translators/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,LastName,FirstName,Prefix")] Translator translator)
    {
      if(ModelState.IsValid)
      {
        translator.Id = Guid.NewGuid();
        _context.Add(translator);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
      }

      return View(translator);
    }

    // GET: Translators/Edit/5
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(Guid? id)
    {
      if(id == null)
      {
        return NotFound();
      }

      var translator = await _context.Translators.FindAsync(id);
      if(translator == null)
      {
        return NotFound();
      }

      return View(translator);
    }

    // POST: Translators/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("Id,LastName,FirstName,Prefix")] Translator translator)
    {
      if(id != translator.Id)
      {
        return NotFound();
      }

      if(ModelState.IsValid)
      {
        try
        {
          _context.Update(translator);
          await _context.SaveChangesAsync();
        }
        catch(DbUpdateConcurrencyException)
        {
          if(!TranslatorExists(translator.Id))
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

      return View(translator);
    }

    // GET: Translators/Delete/5
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(Guid? id)
    {
      if(id == null)
      {
        return NotFound();
      }

      var translator = await _context.Translators
        .FirstOrDefaultAsync(m => m.Id == id);
      if(translator == null)
      {
        return NotFound();
      }

      return View(translator);
    }

    // POST: Translators/Delete/5
    [Authorize(Roles = "Admin")]
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
      var translator = await _context.Translators.FindAsync(id);
      _context.Translators.Remove(translator);
      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }

    private bool TranslatorExists(Guid id)
    {
      return _context.Translators.Any(e => e.Id == id);
    }
  }
}