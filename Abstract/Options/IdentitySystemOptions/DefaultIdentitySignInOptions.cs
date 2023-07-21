namespace Abstract.Options.IdentitySystemOptions
{
    public sealed class DefaultIdentitySignInOptions
    {
        public bool RequireConfirmedPhoneNumber { get; set; }

        public bool RequireConfirmedAccount { get; set; }

        public bool RequireConfirmedEmail { get; set; }
    }
}