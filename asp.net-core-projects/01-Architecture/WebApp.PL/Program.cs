using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore;

namespace WebApp.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebHost.CreateDefaultBuilder(args)
                .ConfigureServices(services => services.AddAutofac())
                .UseStartup<Startup>()
                .Build()
                .Run();
        }        
    }
}
