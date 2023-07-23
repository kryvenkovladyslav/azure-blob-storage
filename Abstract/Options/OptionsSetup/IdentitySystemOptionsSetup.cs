using Abstract.Options.IdentitySystemOptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;

namespace Abstract.Options.OptionsSetup
{
    public sealed class IdentitySystemOptionsSetup : IConfigureNamedOptions<IdentityOptions>
    {
        private readonly DefaultIdentitySytemOptions sytemOptions;

        public IdentitySystemOptionsSetup(IOptions<DefaultIdentitySytemOptions> sytemOptions)
        {
            this.sytemOptions = sytemOptions.Value;
        }

        public void Configure(string name, IdentityOptions options)
        {
            this.ConfigureUserOptions(options, this.sytemOptions.UserOptions);
            this.ConfigureLockoutOptions(options, this.sytemOptions.LockoutOptions);
            this.ConfigureSignInOptions(options, this.sytemOptions.SignInOptions);
            this.ConfigurePasswordOptions(options, this.sytemOptions.PasswordOptions);
        }

        public void Configure(IdentityOptions options)
        {
            this.Configure(nameof(DefaultIdentitySytemOptions), options);
        }

        private void ConfigureUserOptions(IdentityOptions options, DefaultIdentityUserOptions userOptions) 
        {
            options.User = new UserOptions
            {
                RequireUniqueEmail = this.sytemOptions.UserOptions.RequireUniqueEmail
            };
        }

        private void ConfigureLockoutOptions(IdentityOptions options, DefaultIdentityLockoutOptions lockoutOptions)
        {
            options.Lockout = new LockoutOptions
            {
                AllowedForNewUsers = lockoutOptions.AllowedForNewUsers,
                DefaultLockoutTimeSpan = TimeSpan.FromMinutes(lockoutOptions.DefaultLockoutMinutesTimeSpan),
                MaxFailedAccessAttempts = lockoutOptions.MaxFailedAccessAttempts
            };
        }

        private void ConfigureSignInOptions(IdentityOptions options, DefaultIdentitySignInOptions signInOptions)
        {
            options.SignIn = new SignInOptions
            {
                RequireConfirmedAccount = signInOptions.RequireConfirmedAccount,
                RequireConfirmedEmail = signInOptions.RequireConfirmedEmail,
                RequireConfirmedPhoneNumber = signInOptions.RequireConfirmedPhoneNumber
            };
        }

        private void ConfigurePasswordOptions(IdentityOptions options, DefaultIdentityPasswordOptions passwordOptions)
        {
            options.Password = new PasswordOptions
            {
                RequireDigit = passwordOptions.RequireDigit,
                RequiredLength = passwordOptions.RequiredLength,
                RequireLowercase = passwordOptions.RequireLowercase,
                RequireNonAlphanumeric = passwordOptions.RequireNonAlphanumeric,
                RequireUppercase = passwordOptions.RequireUppercase,
                RequiredUniqueChars = passwordOptions.RequiredUniqueChars
            };
        }
    }
}