namespace Abstract.Options.IdentitySystemOptions
{
    public sealed class DefaultIdentitySystemOptions
    {
        public static string Position { get; private set; } = nameof(DefaultIdentitySystemOptions);

        public DefaultIdentityUserOptions UserOptions { get; set; }

        public DefaultIdentitySignInOptions SignInOptions { get; set; }

        public DefaultIdentityLockoutOptions LockoutOptions { get; set; }

        public DefaultIdentityPasswordOptions PasswordOptions { get; set; }
    }
}