using DataTransfer_App.BLL.Interfaces;
using DataTransfer_App.BLL.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DataTransfer_App.WEB
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddScoped<IPurchaseService, PurchaseService>();
        }
       
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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
                    pattern: "{controller=Purchase}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "Add",
                    pattern: "add",
                    defaults: new { controller = "Purchase", action = "Add" });            
                endpoints.MapControllerRoute(
                    name: "delete",
                    pattern: "delete/{id}",
                    defaults: new { controller = "Purchase", action = "Delete" });
            });
        }
    }
}
