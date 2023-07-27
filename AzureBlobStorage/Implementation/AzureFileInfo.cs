using System;

namespace AzureBlobStorage.Implementation
{
    public class AzureFileInfo
    {
        public string FullyQualifiedUri { get; private set; }
        
        public string Name { get; private set; }

        public AzureFileInfo(string fullyQualifiedUri, string name)
        {
            FullyQualifiedUri = fullyQualifiedUri ?? throw new ArgumentNullException(nameof(fullyQualifiedUri));
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }
    }
}