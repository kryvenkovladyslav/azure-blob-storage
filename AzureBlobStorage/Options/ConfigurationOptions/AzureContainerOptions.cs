using AzureBlobStorage.Options.GeneralOptions;
using System.Collections.Generic;

namespace AzureBlobStorage.Options.ConfigurationOptions
{
    public sealed class AzureContainerOptions
    {
        public ICollection<ContainerOptions> ContainersOptions { get; set; }
    }
}