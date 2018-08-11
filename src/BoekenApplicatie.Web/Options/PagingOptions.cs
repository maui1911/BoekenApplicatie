using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoekenApplicatie.Web.Options
{
    public static class PagingOptions
    {
      public static int PageSize { get; set; } = 20;
      public static int MaxPages { get; set; } = 5;
    }
}
