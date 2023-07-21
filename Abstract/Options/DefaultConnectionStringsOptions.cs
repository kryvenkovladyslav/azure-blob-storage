namespace Abstract.Options
{
    public sealed class DefaultConnectionStringsOptions
    {
        public static string Position { get; } = nameof(DefaultConnectionStringsOptions);

        public string MsSqlServerConnection { get; set; }
    }
}