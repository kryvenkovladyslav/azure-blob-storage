using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abstract.Interfaces
{
    public interface IUserRoleService<TUser, TRole, TKey>
            where TUser : IdentityUser<TKey>
            where TRole : IdentityRole<TKey>
            where TKey : IEquatable<TKey>
    {
        public Task<IdentityResult> AddUserToRoleAsync(TUser user, TRole role);

        public Task<IdentityResult> DeleteFromRoleAsync(TUser user, TRole role);

        public Task<bool> IsUserInRoleAsync(TUser user, TRole role);

        public Task<ICollection<string>> GetUserRolesAsync(TUser user);
    }
}