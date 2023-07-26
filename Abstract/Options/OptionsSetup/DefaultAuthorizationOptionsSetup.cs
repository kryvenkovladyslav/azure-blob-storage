using Abstract.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using System;
using System.Security.Claims;

namespace ServerIdentity.Abstract.SetupOptions
{
    public class DefaultAuthorizationOptionsSetup : IConfigureNamedOptions<AuthorizationOptions>
    {
        public void Configure(string name, AuthorizationOptions options)
        {
            options.AddPolicy(ClaimTypes.Role, builder =>
            {
                builder.RequireClaim(ClaimTypes.Role, Enum.GetName(typeof(ApplicationRoles), ApplicationRoles.User));
            });
        }

        public void Configure(AuthorizationOptions options)
        {
            this.Configure(nameof(AuthorizationOptions), options);
        }
    }
}