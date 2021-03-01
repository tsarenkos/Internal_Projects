using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Microsoft.IdentityModel.Tokens;
using TaskTracker.BL.Interfaces;
using TaskTracker.BL.Services;
using TaskTracker.DAL.Models;
using Microsoft.EntityFrameworkCore;
using TaskTracker.Models;
using Microsoft.OpenApi.Models;
using TaskTracker.Shared.Interfaces;
using TaskTracker.Shared.Services;
using TaskTracker.Web.Services;
using TaskTracker.Storage.Core.Interfaces;
using TaskTracker.DAL.Repositories;

namespace TaskTracker.API
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                   .SetBasePath(env.ContentRootPath)
                   .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                   .AddJsonFile($"appsettings.{Environment.MachineName}.json", optional: true)
                   .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddAuthentication("Bearer")
                 .AddJwtBearer("Bearer", options =>
                 {
                     options.SaveToken = true;
                     options.Authority = "https://localhost:5001";

                     options.TokenValidationParameters = new TokenValidationParameters
                     {
                         ValidateAudience = false,
                     };
                 });

            // adds an authorization policy to make sure the token is for scope 'api1'
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiScope", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "api1");
                });
            });

            services.Configure<OptionsForUploadFiles>(this.Configuration.GetSection("UploadFiles"));
            services.Configure<OptionsForSMTPServer>(this.Configuration.GetSection("SMTPOptions"));

            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(
                Configuration.GetConnectionString("DefaultConnection")));

            services.AddHttpContextAccessor();
            //
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<ISubTaskService, SubTaskService>();
            services.AddScoped<IPriorityServiceBL, PriorityService>();
            services.AddScoped<IFileService, FileService>();
            services.AddSingleton<OptionsForUploadFiles>();
            services.AddScoped<ICheckUserService, CheckUserAccessService>();
            services.AddScoped<IPeriodTypeService, PeriodTypeService>();
            services.AddScoped<ICategoryServiceBL, TaskCategoryService>();
            services.AddScoped<IPriorityServiceBL, PriorityService>();
            services.AddScoped<IRepeatingTaskService, RepeatingTaskService>();
            services.AddSingleton<OptionsForSMTPServer>();
            services.AddSingleton<IMailService, MailService>();
            services.AddScoped<ITaskFriendGrants, TaskFriendGrants>();
            services.AddScoped<ITaskTagServiceBL, TaskTagService>();
            services.AddScoped<IUnitOfWork, EFUnitOfWork>();
            services.AddScoped<IIdentityClient, IdentityClient>();
            services.AddScoped<IMyTokenService, MyTokenService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Swagger", Version = "v1" });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                context.Database.EnsureCreated();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger V1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

