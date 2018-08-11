using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoekenApplicatie.Web.ViewModels
{
    public abstract class BaseListViewModel
    {
      protected BaseListViewModel()
      {
        Paging = new PagingModel();
      }
      public PagingModel Paging { get; set; }

      public class PagingModel
      {
        public int FirstPage { get; set; } = 1;
        public int LastPage { get; set; }
        public int TotalPages { get; set; }
        public int PrevPage { get; set; }
        public int NextPage { get; set; }
        public int CurrentPage { get; set; }
      }
  }
}
