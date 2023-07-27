namespace Abstract.Options.AzureBlobStorageOptions
{
    public sealed class AzureBlobStorageAccessKeyOptions
    {
        public static string Position { get; private set; } = nameof(AzureBlobStorageAccessKeyOptions);

        public string AccessKey { get; set; }
    }
}