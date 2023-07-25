using System.Collections.Generic;

namespace AzureBlobStorage.Options.GeneralOptions
{
    public sealed class ContainerOptions
    {
        public string Name { get; set; }

        public string PublicAccessType { get; set; }

        public ICollection<ContainerPolicyOptions> ContainerPolicy { get; set; }
    }
}