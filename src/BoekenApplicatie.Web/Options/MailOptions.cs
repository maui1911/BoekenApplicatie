using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoekenApplicatie.Web.Options
{
    public class MailOptions
    {
      public string ApiKey { get; set; }
      public string FromAddress { get; set; }
      public string FromAddressName { get; set; }
    }
}
