using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SBAdmin.Web
{
    /// <summary>
    /// Class Startup.
    /// Implements startup features
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Method configures services
        /// </summary>
        /// <param name="services">Services</param>
        public void ConfigureServices(IServiceCollection services)
        {
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
            }
            if (env.IsProduction())
            {
                logger.LogInformation("Production mode");
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
