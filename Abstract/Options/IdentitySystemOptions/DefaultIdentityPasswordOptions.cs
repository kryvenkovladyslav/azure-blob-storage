namespace Abstract.Options.IdentitySystemOptions
{
    public sealed class DefaultIdentityPasswordOptions
    {
        public int RequiredUniqueChars { get; set; }

        public bool RequireNonAlphanumeric { get; set; }

        public int RequiredLength { get; set; }

        public bool RequireDigit { get; set; }

        public bool RequireLowercase { get; set; }

        public bool RequireUppercase { get; set; }
    }
}