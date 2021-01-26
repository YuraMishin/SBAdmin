using System;
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
        private readonly IWebHostEnvironment _env;

        /// <summary>
        /// Configuration
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration">IConfiguration</param>
        /// <param name="env">env</param>
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            _env = env;
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

            services.AddIdentity<User, IdentityRole>(options =>
                {
                    options.Password.RequiredLength = 4;
                })
                .AddEntityFrameworkStores<AppDbContext>();

            // DB connection
            if (_env.IsDevelopment())
            {
                services.AddDbContext<AppDbContext>(
                    option =>
                    {
                        option.EnableSensitiveDataLogging();
                        option.EnableDetailedErrors();
                        option.UseNpgsql(
                            Configuration.GetConnectionString("SBAdmin.dev"));
                    });
            }

            if (_env.IsProduction())
            {
                services.AddDbContext<AppDbContext>(
                    option =>
                    {
                        option.EnableSensitiveDataLogging();
                        option.EnableDetailedErrors();
                        option.UseNpgsql(
                            Configuration.GetConnectionString("SBAdmin.prod"));
                    });
            }

            #region Dependency Injection

            services.AddScoped<RoleManager<IdentityRole>>();

            #endregion

            services.AddControllersWithViews();
            services.AddRazorPages().AddRazorRuntimeCompilation();

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
                options.LoginPath = "/Admin/Login";
                options.SlidingExpiration = true;
            });
        }

        /// <summary>
        /// Method gets called by the runtime to configure
        /// the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="logger"></param>
        public void Configure(
            IApplicationBuilder app,
            ILogger<Startup> logger)
        {
            logger.LogInformation("App is running. Enjoy!");
            if (_env.IsDevelopment())
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
                        Path.Combine(_env.ContentRootPath, "node_modules")),
                    RequestPath = "/node_modules"
                });
            }
            if (_env.IsProduction())
            {
                logger.LogInformation("Production mode");
            }

            // add support folder
            app.UseStaticFiles();



            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

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
