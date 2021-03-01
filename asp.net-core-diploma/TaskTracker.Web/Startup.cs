using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net;
using TaskTracker.Shared.Interfaces;
using TaskTracker.Shared.Services;
using TaskTracker.Web.Interfaces;
using TaskTracker.Web.Services;

namespace TaskTracker.Web
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
            services.AddHttpContextAccessor();
            services.AddTransient<IMyTokenService, MyTokenService>();
            services.AddTransient<IAPIClient, APIClient>();
            services.AddTransient<IIdentityClient, IdentityClient>();
            services.AddTransient<IMyTaskFilterService, MyTaskFilterService>();

            services.AddAuthentication(o => {
                o.DefaultScheme = "TokenAuthenticationScheme";
            })
            .AddScheme<TokenAuthenticationOptions, TokenAuthenticationHandler>("TokenAuthenticationScheme", o => { });
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
                app.UseHsts();
            }

            app.UseStatusCodePages(async context => {
                var response = context.HttpContext.Response;

                if (response.StatusCode == (int)HttpStatusCode.Unauthorized)
                {
                    response.Redirect("/account/login/?ReturnUrl="+ context.HttpContext.Request.Path + context.HttpContext.Request.QueryString);
                }
            });

            app.UseHttpsRedirection();
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
