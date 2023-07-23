using Abstract.Common;
using Abstract.Options;
using IdentityDataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Security.Claims;

namespace IdentityDataAccessLayer.Data
{
    public static class DatabaseInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetService<RoleManager<Role>>();

            if (roleManager.Roles.Count() == 0)
            {
                SeedApplicationRolesWithClaims(roleManager);
            }

            var userManager = serviceProvider.GetService<UserManager<User>>();
            var seedUserOptions = serviceProvider.GetRequiredService<IOptions<DefaultSeedUserOptions>>().Value;

            if (userManager.FindByNameAsync(seedUserOptions.UserName).GetAwaiter().GetResult() == null)
            {
                SeedApplicationUserWithClaims(userManager, seedUserOptions);
            }
        }

        private static void SeedApplicationRolesWithClaims(RoleManager<Role> roleManager)
        {
            var roleNames = Enum.GetNames(typeof(ApplicationRoles));

            foreach (var name in roleNames)
            {
                var role = new Role { Name = name };
                var userRoleCreation = roleManager.CreateAsync(role).GetAwaiter().GetResult();

                if (userRoleCreation.Succeeded)
                {
                    roleManager.AddClaimAsync(role, new Claim(ClaimTypes.Role, name)).GetAwaiter().GetResult();
                }
            }
        }

        private static void SeedApplicationUserWithClaims(UserManager<User> userManager, DefaultSeedUserOptions options)
        {
            var applicationUser = new User
            {
                FirstName = options.FirstName,
                LastName = options.LastName,
                UserName = options.UserName,
                Email = options.Email,
            };

            var creationUserResult = userManager.CreateAsync(applicationUser, options.Password).GetAwaiter().GetResult();

            if (creationUserResult.Succeeded)
            {
                ApplyClaimsRoles(userManager, applicationUser, ApplicationRoles.Administrator, ApplicationRoles.Developer);
            }
        }

        private static void ApplyClaimsRoles(UserManager<User> userManager, User user, params ApplicationRoles[] roles)
        {
            roles.ToList().ForEach(role =>
            {
                var currentRule = Enum.GetName(typeof(ApplicationRoles), role);
                userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, currentRule)).GetAwaiter().GetResult();
                userManager.AddToRoleAsync(user, currentRule).GetAwaiter().GetResult();
            });
        }
    }
}