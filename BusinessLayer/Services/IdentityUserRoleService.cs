using Abstract.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class IdentityUserRoleService<TUser, TRole, TKey> : IUserRoleService<TUser, TRole, TKey>
            where TUser : IdentityUser<TKey>
            where TRole : IdentityRole<TKey>
            where TKey : IEquatable<TKey>
    {
        private readonly UserManager<TUser> userManager;

        public IdentityUserRoleService(UserManager<TUser> userManager)
        {
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public virtual async Task<IdentityResult> AddUserToRoleAsync(TUser user, TRole role)
        {
            this.ThrowExceptionIfArgumentNull(user, role);

            await this.userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, role.Name));
            var roleResult = await this.userManager.AddToRoleAsync(user, role.Name);

            if (!roleResult.Succeeded)
            {
                return roleResult;
            }

            return roleResult;
        }

        public virtual async Task<IdentityResult> DeleteFromRoleAsync(TUser user, TRole role)
        {
            this.ThrowExceptionIfArgumentNull(user, role);

            await this.userManager.RemoveClaimAsync(user, new Claim(ClaimTypes.Role, role.Name));

            var roleResult = await this.userManager.RemoveFromRoleAsync(user, role.Name);

            if (!roleResult.Succeeded)
            {
                return roleResult;
            }

            return roleResult;
        }

        public virtual async Task<bool> IsUserInRoleAsync(TUser user, TRole role)
        {
            this.ThrowExceptionIfArgumentNull(user, role);

            return await this.userManager.IsInRoleAsync(user, role.Name);
        }

        public virtual async Task<ICollection<string>> GetUserRolesAsync(TUser user)
        {
            this.ThrowExceptionIfUserEmpty(user);

            return await this.userManager.GetRolesAsync(user) ?? new List<string>();
        }

        private void ThrowExceptionIfArgumentNull(TUser user, TRole role)
        {
            this.ThrowExceptionIfUserEmpty(user);
            this.ThrowExceptionIfRoleEmpty(role);
        }

        private TUser ThrowExceptionIfUserEmpty(TUser user)
        {
            return user ?? throw new ArgumentNullException(nameof(user));
        }

        private TRole ThrowExceptionIfRoleEmpty(TRole role)
        {
            return role ?? throw new ArgumentNullException(nameof(role));
        }
    }
}