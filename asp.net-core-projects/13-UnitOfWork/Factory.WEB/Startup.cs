using Factory.BLL.Interfaces;
using Factory.BLL.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Autofac;
using Factory.BLL.Infrastructure;

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
            services.AddControllersWithViews();
            services.AddScoped<IFactoryService, FactoryService>();            
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
                    pattern: "{controller=Factory}/{action=GetEmployees}/{id?}");
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
            });
        }
        
    }
}
