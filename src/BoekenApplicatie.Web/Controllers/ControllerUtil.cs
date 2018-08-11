using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoekenApplicatie.Web.ViewModels;

namespace BoekenApplicatie.Web.Controllers
{
    public static class ControllerUtil
    {
      public static void SetPagingModel(BaseListViewModel.PagingModel paging, int page, int count, int size)
      {
        paging.TotalPages = (int)Math.Ceiling(decimal.Divide(count, size));
        paging.LastPage = paging.TotalPages;
        paging.PrevPage = Math.Max(page - 1, paging.FirstPage);
        paging.NextPage = Math.Min(page + 1, paging.LastPage);
        paging.CurrentPage = page;
      }
  }
}
