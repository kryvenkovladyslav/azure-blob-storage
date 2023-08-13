namespace Abstract.Options.IdentitySystemOptions
{
    public sealed class DefaultIdentityLockoutOptions
    {
        public bool AllowedForNewUsers { get; set; }

        public int MaxFailedAccessAttempts { get; set; }

        public int DefaultLockoutMinutesTimeSpan { get; set; }
    }
}