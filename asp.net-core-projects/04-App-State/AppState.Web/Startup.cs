using System;
using AppState.Core.Interfaces;
using AppState.Infrastructure.Services;
using AppState.Web.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AppState.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }
        private readonly MySessionOptions cookieOptions;

        public Startup()
        {
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            Configuration = builder.Build();
            cookieOptions = Configuration.GetSection(MySessionOptions.SECTION).Get<MySessionOptions>();
        }        
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddHttpContextAccessor();
            services.AddDistributedMemoryCache();
            services.AddSession(options => {
                options.Cookie.Name = cookieOptions.Name;
                options.IdleTimeout = TimeSpan.FromSeconds(cookieOptions.IdleTimeout);
            });
            services.AddScoped<ISessionService, SessionService>();
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

            app.UseSession();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "getAllSessions",
                    pattern: "get",
                    defaults: new { controller = "Session", action = "GetAll" });

                endpoints.MapControllerRoute(
                    name: "getValue",
                    pattern: "get/{key}",
                    defaults: new { controller = "Session", action = "GetByKey" });

                endpoints.MapControllerRoute(
                    name: "setKeyValue",
                    pattern: "set/{key}/{value}",
                    defaults: new { controller = "Session", action = "SetKey" });

                endpoints.MapControllerRoute(
                    name: "deleteKey",
                    pattern: "delete/{key}",
                    defaults: new { controller = "Session", action = "DeleteKey" });
            });
        }
    }
}
