using System.Collections.Generic;

namespace Abstract.Options.AzureBlobStorageOptions
{
    public sealed class AzureBlobStorageContainerOptions
    {
        public static string Position { get; private set; } = nameof(AzureBlobStorageContainerOptions);

        public ICollection<DefaultContainerOptions> Containers { get; set; }
    }
}