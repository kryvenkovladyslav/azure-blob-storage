using AzureBlobStorage.Extensions;
using DependencyInjection.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using WebApplication.Infrastructure.Extensions;
using WebApplication.Infrastructure.Middleware;

namespace WebApplication.Startup
{
    public class StartupDevelopment
    {
        public IConfiguration Configuration { get; }

        public StartupDevelopment(IConfiguration configuration)
        {
            this.Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureApplicationOptions(this.Configuration);
            services.AddApplicationIdentityDataAccessLayer();
            services.AddApplicationBusinessSerivces();
            services.AddAzureBlobStorage();
            services.AddMappingProfiles();

            services.AddAuthentication();
            services.AddAuthorization();

            services.AddControllersWithViews().AddRazorRuntimeCompilation();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ExceptionHandler>();

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

                endpoints.MapFallbackToController("InternalErrorHandler", "Home");
            });
        }
    }
}