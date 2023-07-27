using Abstract.Options.AzureBlobStorageOptions;
using AzureBlobStorage.Options.ConfigurationOptions;
using Microsoft.Extensions.Options;

namespace Abstract.Options.OptionsSetup
{
    public sealed class AzureAccessKeyOptionsSetup : IConfigureNamedOptions<AzureAccessKeyOptions>
    {
        private AzureBlobStorageAccessKeyOptions options;

        public AzureAccessKeyOptionsSetup(IOptions<AzureBlobStorageAccessKeyOptions> options)
        {
            this.options = options.Value;
        }

        public void Configure(string name, AzureAccessKeyOptions options)
        {
            options.ConnectionString = this.options.AccessKey;
        }

        public void Configure(AzureAccessKeyOptions options)
        {
            this.Configure(nameof(AzureAccessKeyOptionsSetup), options);
        }
    }
}