using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(BoekenApplicatie.Web.Areas.Identity.IdentityHostingStartup))]
namespace BoekenApplicatie.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}