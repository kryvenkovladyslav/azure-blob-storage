using IdentityDataAccessLayer.Data;
using IdentityDataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityDataAccessLayer.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddIdentityDataAccessLayer(this IServiceCollection services)
        {
            services.AddDbContext<IdentityContext>();

            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders();

            return services;
        }
    }
}