using System;
using System.Linq;

namespace BoekenApplicatie.Web.Options
{
  public class SortFilterPageOptions
  {
    public string OrderByProp { get; set; }
    public bool OrderByDesc { get; set; }
    public string[] FilterByProp { get; set; } = { };
    public string[] FilterValue { get; set; } = { };
    public int CurrentPage { get; set; }
    public int PageSize { get; set; } = 20;
    public int MaxPages { get; set; } = 5;

    /// <summary>
    /// This is set to the number of pages available based on the number of entries in the query
    /// </summary>
    public int PagesCount { get; private set; }

    /// <summary>
    /// This holds the state of the key parts of the SortFilterPage parts 
    /// </summary>
    public string PrevStateCheck { get; set; }

    public void SetupRestOfDto<T>(IQueryable<T> query)
    {
      var queryCount = (decimal) query.Count();
      if (queryCount > 0)
      {
        PagesCount = (int) Math.Ceiling(queryCount / PageSize);
      }

      CurrentPage = Math.Min(Math.Max(1, CurrentPage), PagesCount);

      var newStateCheck = GetStateCheck();
      if (PrevStateCheck != newStateCheck) CurrentPage = 1;

      PrevStateCheck = newStateCheck;
    }

    private string GetStateCheck()
    {
      return $"{FilterByProp},{FilterValue},{PageSize},{PagesCount}";
    }
  }
}