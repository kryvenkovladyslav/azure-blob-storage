namespace Abstract.Options.ConnectionOptions
{
    public sealed class DefaultConnectionIdentityDatabaseOptions
    {
        public static string Position { get; } = nameof(DefaultConnectionIdentityDatabaseOptions);

        public string MsSqlServerConnection { get; set; }
    }
}