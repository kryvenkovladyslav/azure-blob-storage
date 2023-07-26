using Abstract.Interfaces;
using Abstract.Models;
using BusinessLayer.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace BusinessLayer.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBusinessLayerServices(this IServiceCollection services)
        {
            services.TryAddScoped<IUserService<ApplicationUser, Guid>, IdentityUserService<ApplicationUser, Guid>>();
            services.TryAddScoped<IAuthenticationService<ApplicationUser, Guid>, AuthenticationService<ApplicationUser, Guid>>();
            services.TryAddScoped<IUserRoleService<ApplicationUser, ApplicationRole, Guid>, IdentityUserRoleService<ApplicationUser, ApplicationRole, Guid>>();

            return services;
        }
    }
}