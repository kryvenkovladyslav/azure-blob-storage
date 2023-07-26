using Abstract.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class AuthenticationService<TUser, TKey> : IAuthenticationService<TUser, TKey>
        where TUser : IdentityUser<TKey>
        where TKey : IEquatable<TKey>
    {
        private readonly SignInManager<TUser> signInManager;

        public AuthenticationService(SignInManager<TUser> signInManager)
        {
            this.signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        }

        public virtual async Task<SignInResult> SignInWithPasswordAsync(TUser user, string password)
        {
            return await this.signInManager.PasswordSignInAsync(user, password, false, false);
        }

        public virtual async Task LogoutAsync()
        {
            await this.signInManager.SignOutAsync();
        }
    }
}