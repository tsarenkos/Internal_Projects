using Autofac;
using FactoryApp.BLL.Infrastructure;
using FactoryApp.BLL.Interfaces;
using FactoryApp.BLL.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace FactoryApp.Web
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
            services.AddControllersWithViews();
            services.AddScoped<IDelivererService, DelivererService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IMachineService, MachineService>();
            services.AddScoped<IRequestService, RequestService>();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutofacModule(Configuration.GetConnectionString("DefaultConnection")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
                    pattern: "{controller=Machine}/{action=GetMachines}/{id?}");
                endpoints.MapControllerRoute(
                    name: "employees",
                    pattern: "employees",
                    defaults: new { controller = "Employee", action = "GetEmployees" });
                endpoints.MapControllerRoute(
                    name: "machines",
                    pattern: "machines",
                    defaults: new { controller = "Machine", action = "GetMachines"});
                endpoints.MapControllerRoute(
                    name: "adddeliverer",
                    pattern: "adddeliverer",
                    defaults: new { controller = "Deliverer", action = "AddDeliverer" });
                endpoints.MapControllerRoute(
                    name: "addrequest",
                    pattern: "addrequest",
                    defaults: new { controller = "Request", action = "AddRequest" });
                endpoints.MapControllerRoute(
                    name: "addhandler",
                    pattern: "addhandler",
                    defaults: new { controller = "Request", action = "AddRequestHandler" });
            });
        }
    }
}
