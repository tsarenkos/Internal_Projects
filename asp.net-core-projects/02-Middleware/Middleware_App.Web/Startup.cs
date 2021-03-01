using Middleware_App.Core.BusinessModels;
using Middleware_App.Core.BusinessServices;
using Middleware_App.Core.Interfaces;
using Middleware_App.Web.Components;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Middleware_App.Web
{
    public class Startup
    {
        public IConfiguration AppConfiguration { get; set; }

        public Startup()
        {
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            AppConfiguration = builder.Build();
        }        
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<QueryOptions>(AppConfiguration.GetSection("QueryModel"));
            services.AddSingleton<ICheckQueryService, CheckQueryService>();
            services.AddControllersWithViews();
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<CheckLengthMiddleware>();

            app.UseMiddleware<CheckContentMiddleware>();

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
