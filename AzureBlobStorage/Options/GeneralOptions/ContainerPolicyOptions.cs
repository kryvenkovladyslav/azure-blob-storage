using System;

namespace AzureBlobStorage.Options.GeneralOptions
{
    public sealed class ContainerPolicyOptions
    {
        public string Identifier { get; set; }

        public DateTime StartsOn { get; set; }

        public DateTime EndsOn { get; set; }

        public string Permissions { get; set; }
    }
}