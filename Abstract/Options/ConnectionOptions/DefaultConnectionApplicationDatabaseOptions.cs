namespace Abstract.Options.ConnectionOptions
{
    public sealed class DefaultConnectionApplicationDatabaseOptions
    {
        public static string Position { get; } = nameof(DefaultConnectionApplicationDatabaseOptions);

        public string MsSqlServerConnection { get; set; }
    }
}