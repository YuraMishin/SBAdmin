using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SBAdmin.Web.Data;
using SBAdmin.Web.Models;

namespace SBAdmin.Web
{
    /// <summary>
    /// Class Startup.
    /// Implements startup features
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Configuration
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration">IConfiguration</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Method configures services
        /// </summary>
        /// <param name="services">Services</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Authentication
            services.AddAuthentication();
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>();

            // DB connection
            services.AddDbContext<AppDbContext>(
                option =>
                {
                    option.EnableSensitiveDataLogging();
                    option.EnableDetailedErrors();
                    option.UseNpgsql(
                        Configuration.GetConnectionString("SBAdmin.dev"));
                });

            services.AddControllersWithViews();
            services.AddRazorPages().AddRazorRuntimeCompilation();
        }

        /// <summary>
        /// Method gets called by the runtime to configure
        /// the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="logger"></param>
        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env,
            ILogger<Startup> logger)
        {
            logger.LogInformation("App is running. Enjoy!");
            if (env.IsDevelopment())
            {
                logger.LogInformation("Development mode");
                app.UseDeveloperExceptionPage();
                app.UseStaticFiles(new StaticFileOptions
                {
                    FileProvider = new PhysicalFileProvider(
                        Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
                    RequestPath = "/Resources"
                });
                app.UseStaticFiles(new StaticFileOptions
                {
                    FileProvider = new PhysicalFileProvider(
                        Path.Combine(env.ContentRootPath, "node_modules")),
                    RequestPath = "/node_modules"
                });
            }
            if (env.IsProduction())
            {
                logger.LogInformation("Production mode");
            }

            // add support folder
            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapGet("/hello", async context =>
                {
                    await context.Response.WriteAsync("Hello ASP.Net5 !");
                });

                endpoints.MapRazorPages();
            });
        }
    }
}
