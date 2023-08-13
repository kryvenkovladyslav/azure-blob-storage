using AzureBlobStorage.Abstract;
using AzureBlobStorage.Implementation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace AzureBlobStorage.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAzureBlobStorage(this IServiceCollection services)
        {
            services.TryAddSingleton<IAuthenticator, AzureAccessKeyAuthenticator>();
            services.TryAddScoped<IAzureBlobStorageService, AzureBlobStorageService>();
            services.TryAddSingleton<IAzureContainerConfigurationManager, AzureContainerConfigurationManager>();

            ConfigureStorage(services);

            return services;
        }

        private static void ConfigureStorage(IServiceCollection services)
        {
            services
                .BuildServiceProvider()
                .GetRequiredService<IAzureContainerConfigurationManager>()
                .Configure();
        }
    }
}