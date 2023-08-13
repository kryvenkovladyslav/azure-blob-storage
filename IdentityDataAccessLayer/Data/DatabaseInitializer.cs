using Abstract.Common;
using Abstract.Models;
using Abstract.Options.SeedDatabaseOptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace IdentityDataAccessLayer.Data
{
    public static class DatabaseInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetService<RoleManager<ApplicationRole>>();

            if (roleManager.Roles.Count() == 0)
            {
                SeedApplicationRolesWithClaims(roleManager);
            }

            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();
            var databaseSeed = serviceProvider.GetRequiredService<IOptions<DefaultSeedIdentityDatabaseOptions>>().Value;

            databaseSeed.Users.ToList().ForEach(user =>
            {
                if (userManager.FindByNameAsync(user.UserName).GetAwaiter().GetResult() == null)
                {
                    SeedApplicationUserWithClaims(userManager, user);
                }
            });
        }

        private static void SeedApplicationRolesWithClaims(RoleManager<ApplicationRole> roleManager)
        {
            var roleNames = Enum.GetNames(typeof(ApplicationRoles));

            foreach (var name in roleNames)
            {
                var role = new ApplicationRole(name);
                var userRoleCreation = roleManager.CreateAsync(role).GetAwaiter().GetResult();

                if (userRoleCreation.Succeeded)
                {
                    roleManager.AddClaimAsync(role, new Claim(ClaimTypes.Role, name)).GetAwaiter().GetResult();
                }
            }
        }

        private static void SeedApplicationUserWithClaims(UserManager<ApplicationUser> userManager, DefaultSeedUserOptions options)
        {
            var applicationUser = new ApplicationUser
            {
                FirstName = options.FirstName,
                LastName = options.LastName,
                UserName = options.UserName,
                Email = options.Email,
            };

            var creationUserResult = userManager.CreateAsync(applicationUser, options.Password).GetAwaiter().GetResult();

            if (creationUserResult.Succeeded)
            {
                ApplyClaimsRoles(userManager, applicationUser, options.Roles);
            }
        }

        private static void ApplyClaimsRoles(UserManager<ApplicationUser> userManager, ApplicationUser user, ICollection<ApplicationRoles> roles)
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