using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace Abstract.Interfaces
{
    public interface IAuthenticationService<TUser, TKey>
        where TUser : IdentityUser<TKey>
        where TKey : IEquatable<TKey>
    {
        public Task<SignInResult> SignInWithPasswordAsync(TUser user, string password);

        public Task LogoutAsync();
    }
}