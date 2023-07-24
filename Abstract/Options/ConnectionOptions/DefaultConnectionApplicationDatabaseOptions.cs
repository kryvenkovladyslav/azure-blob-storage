namespace Abstract.Options.ConnectionOptions
{
    internal class DefaultConnectionApplicationDatabaseOptions
    {
        public static string Position { get; } = nameof(DefaultConnectionApplicationDatabaseOptions);

        public string MsSqlServerConnection { get; set; }
    }
}