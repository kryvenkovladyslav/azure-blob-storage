using Abstract.Options.AzureBlobStorageOptions;
using AzureBlobStorage.Options.ConfigurationOptions;
using AzureBlobStorage.Options.GeneralOptions;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;

namespace Abstract.Options.OptionsSetup
{
    public sealed class AzureBlobStorageContainerOptionsSetup : IConfigureNamedOptions<AzureContainerOptions>
    {
        private readonly AzureBlobStorageContainerOptions options;
        public AzureBlobStorageContainerOptionsSetup(IOptions<AzureBlobStorageContainerOptions> options)
        {
            this.options = options.Value;
        }

        public void Configure(string name, AzureContainerOptions options)
        {
            options.ContainersOptions = new List<ContainerOptions>();

            this.options.Containers.ToList().ForEach(option =>
            {
                var policy = new List<ContainerPolicyOptions>();

                option.PolicyOptions.ToList().ForEach(policyItem =>
                {
                    policy.Add(new ContainerPolicyOptions
                    {
                        Identifier = policyItem.Identifier,
                        Permissions = policyItem.Permissions
                    });
                });

                options.ContainersOptions.Add(new ContainerOptions { Name = option.Name, ContainerPolicy = policy });
            });
        }

        public void Configure(AzureContainerOptions options)
        {
            this.Configure(nameof(AzureBlobStorageContainerOptions), options);
        }
    }
}