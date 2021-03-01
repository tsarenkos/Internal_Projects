using Factory.BLL.Interfaces;
using Factory.BLL.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Autofac;
using Factory.BLL.Infrastructure;
using Factory.WEB.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Factory.WEB.IdentityServices;

namespace Factory.WEB
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();            
            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddScoped<IEmployeesService, EmployeesService>();
            services.AddScoped<IDelivererService, DelivererService>();
            services.AddScoped<IMachinesService, MachinesService>();
            services.AddScoped<IRequestService, RequestService>();
            services.AddScoped<IRoleService, RoleService>();            
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutofacModule(Configuration.GetSection("Database").GetSection("DAL").Value,
                Configuration.GetSection("Database").GetSection("ConnectionString").Value));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "employees",
                    pattern: "employees",
                    defaults: new { controller = "Factory", action = "GetEmployees" });
                endpoints.MapControllerRoute(
                    name: "machines",
                    pattern: "machines",
                    defaults: new { controller = "Factory", action = "GetMachines" });
                endpoints.MapControllerRoute(
                    name: "adddeliverer",
                    pattern: "adddeliverer",
                    defaults: new { controller = "Factory", action = "AddDeliverer" });
                endpoints.MapControllerRoute(
                    name: "addrequest",
                    pattern: "addrequest",
                    defaults: new { controller = "Factory", action = "AddRequest" });
                endpoints.MapControllerRoute(
                    name: "addrequesthandler",
                    pattern: "addrequesthandler",
                    defaults: new { controller = "Factory", action = "AddRequestHandler" });
                endpoints.MapRazorPages();
            });
        }
    }
}
