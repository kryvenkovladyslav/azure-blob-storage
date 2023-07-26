using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;

namespace Abstract.Options.OptionsSetup
{
    public sealed class DefaultAuthenticationOptionsSetup : IConfigureNamedOptions<AuthenticationOptions>
    {
        public void Configure(string name, AuthenticationOptions options)
        {
            options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        }

        public void Configure(AuthenticationOptions options)
        {
            this.Configure(nameof(AuthenticationOptions), options);
        }
    }
}