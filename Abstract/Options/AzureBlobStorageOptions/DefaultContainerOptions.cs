using System.Collections.Generic;

namespace Abstract.Options.AzureBlobStorageOptions
{
    public sealed class DefaultContainerOptions
    {
        public string Name { get; set; }

        public ICollection<PolicyOptions> PolicyOptions { get; set; }
    }
}