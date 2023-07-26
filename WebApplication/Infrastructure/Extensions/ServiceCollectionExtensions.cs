using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
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
    }
}