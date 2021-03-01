using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Claims_App.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Claims_App.IdentityServices;
using Claims_App.Requirements;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Claims_App.ClaimMiddlewares;

namespace Claims_App
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            
            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthorizationHandler, AccessFailedCountHandler>();
            services.AddScoped<IAuthorizationHandler, CountryHandler>();

            services.AddAuthorizationCore(options =>
            {
                options.AddPolicy("phone exists", policy =>
                {
                    policy.RequireClaim(ClaimTypes.MobilePhone, "true");
                });
                options.AddPolicy("phone or email confirmed", policy =>
                {
                    policy.RequireClaim("phone or email confirmed", "true");
                });
                options.AddPolicy("access failed limit", policy =>
                {
                    policy.Requirements.Add(new AccessFailedCountRequirement(10));
                });
                options.AddPolicy("Belarus", policy =>
                {
                    policy.Requirements.Add(new CountryRequirement("Belarus"));
                });
            });
        }       
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");                
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseMiddleware<PhoneExistsClaimMiddleware>();
            app.UseMiddleware<PhoneOrEmailConfirmedClaimMiddleware>();
            app.UseMiddleware<AccessFailedCountClaim>();
            app.UseMiddleware<CountryClaimMiddleware>();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
