using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BoekenApplicatie.Web.Options;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace BoekenApplicatie.Web.Services
{
    public class SendGridMailService : IMailservice
    {
      private readonly MailOptions _options;
      public SendGridMailService(IOptions<MailOptions> options)
      {
        _options = options.Value;
      }
      public async Task SendMailAsync(string email, string subject, string htmlContent)
      {
        var client = new SendGridClient(_options.ApiKey);
        var from = new EmailAddress(_options.FromAddress, _options.FromAddressName);
        var to = new EmailAddress(email);
        var plainTextContent = Regex.Replace(htmlContent, "<[^>]*>", "");
        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

        await client.SendEmailAsync(msg);
    }
    }
}
