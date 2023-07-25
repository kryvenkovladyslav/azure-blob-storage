namespace Abstract.Options.ConnectionOptions
{
    public class DefaultConnectionApplicationDatabaseOptions
    {
        public static string Position { get; } = nameof(DefaultConnectionApplicationDatabaseOptions);

        public string MsSqlServerConnection { get; set; }
    }
}