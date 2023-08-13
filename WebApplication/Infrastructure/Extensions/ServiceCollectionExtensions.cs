using Abstract.Options.AzureBlobStorageOptions;
using Abstract.Options.ConnectionOptions;
using Abstract.Options.IdentitySystemOptions;
using Abstract.Options.OptionsSetup;
using Abstract.Options.SeedDatabaseOptions;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServerIdentity.Abstract.SetupOptions;
using WebApplication.Infrastructure.MappingProfiles;

namespace WebApplication.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMappingProfiles(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile<UserProfile>();
            });

            services.AddSingleton(mapperConfig.CreateMapper());
            return services;
        }

        public static IServiceCollection ConfigureApplicationOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DefaultConnectionIdentityDatabaseOptions>(configuration.GetSection(DefaultConnectionIdentityDatabaseOptions.Position));
            services.Configure<DefaultSeedIdentityDatabaseOptions>(configuration.GetSection(DefaultSeedIdentityDatabaseOptions.Position));
            services.Configure<AzureBlobStorageContainerOptions>(configuration.GetSection(AzureBlobStorageContainerOptions.Position));
            services.Configure<AzureBlobStorageAccessKeyOptions>(configuration.GetSection(AzureBlobStorageAccessKeyOptions.Position));
            services.Configure<DefaultIdentitySystemOptions>(configuration.GetSection(DefaultIdentitySystemOptions.Position));

            services.ConfigureOptions<AzureBlobStorageContainerOptionsSetup>();
            services.ConfigureOptions<DefaultAuthenticationOptionsSetup>();
            services.ConfigureOptions<DefaultAuthorizationOptionsSetup>();
            services.ConfigureOptions<AzureAccessKeyOptionsSetup>();
            services.ConfigureOptions<IdentitySystemOptionsSetup>();

            return services;
        }
    }
}