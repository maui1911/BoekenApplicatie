using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoekenApplicatie.Web.Services
{
    public interface IMailservice
    {
      Task SendMailAsync(string email, string subject, string htmlContent);
    }
}
