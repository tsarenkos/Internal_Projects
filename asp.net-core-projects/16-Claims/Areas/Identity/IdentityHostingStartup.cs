using Microsoft.AspNetCore.Hosting;


[assembly: HostingStartup(typeof(Claims_App.Areas.Identity.IdentityHostingStartup))]
namespace Claims_App.Areas.Identity
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