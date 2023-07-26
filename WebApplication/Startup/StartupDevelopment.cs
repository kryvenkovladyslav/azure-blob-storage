using Abstract.Options.ConnectionOptions;
using Abstract.Options.IdentitySystemOptions;
using Abstract.Options.OptionsSetup;
using Abstract.Options.SeedDatabaseOptions;
using DependencyInjection.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServerIdentity.Abstract.SetupOptions;
using System;
using WebApplication.Infrastructure.Extensions;

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
            services.Configure<DefaultConnectionIdentityDatabaseOptions>(this.Configuration.GetSection(DefaultConnectionIdentityDatabaseOptions.Position));
            services.Configure<DefaultIdentitySystemOptions>(this.Configuration.GetSection(DefaultIdentitySystemOptions.Position));
            services.Configure<DefaultSeedIdentityDatabaseOptions>(this.Configuration.GetSection(DefaultSeedIdentityDatabaseOptions.Position));
            services.ConfigureOptions<IdentitySystemOptionsSetup>();
            services.ConfigureOptions<DefaultAuthenticationOptionsSetup>();
            services.ConfigureOptions<DefaultAuthorizationOptionsSetup>();

            services.AddApplicationBusinessSerivces();
            services.AddApplicationIdentityDataAccessLayer();
            services.AddMappingProfiles();

            services.AddAuthentication();
            services.AddAuthorization();

            services.AddControllersWithViews().AddRazorRuntimeCompilation();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
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
            });
        }
    }
}