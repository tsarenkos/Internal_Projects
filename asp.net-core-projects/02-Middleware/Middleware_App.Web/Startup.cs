using Middleware_App.Core.Services;
using Middleware_App.Core.Interfaces;
using Middleware_App.Web.Components;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Middleware_App.Web.Models;

namespace Middleware_App.Web
{
    public class Startup
    {
        public IConfiguration AppConfiguration { get; set; }

        public Startup()
        {
            AppConfiguration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();            
        }        
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<QueryOptions>(AppConfiguration.GetSection(QueryOptions.Section));
            services.AddSingleton<ICheckQueryService, CheckQueryService>();
            services.AddControllersWithViews();
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCheckQueryLength();
            app.UseCheckQueryContent();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
