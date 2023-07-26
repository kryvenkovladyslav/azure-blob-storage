using BusinessLayer.Extensions;
using IdentityDataAccessLayer.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace DependencyInjection.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationBusinessSerivces(this IServiceCollection services)
        {
            services.AddBusinessLayerServices();
            return services;
        }

        public static IServiceCollection AddApplicationIdentityDataAccessLayer(this IServiceCollection services)
        {
            services.AddIdentityDataAccessLayer();
            return services;
        }
    }
}