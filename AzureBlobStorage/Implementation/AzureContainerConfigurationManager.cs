using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using AzureBlobStorage.Abstract;
using AzureBlobStorage.Options.ConfigurationOptions;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AzureBlobStorage.Implementation
{
    public class AzureContainerConfigurationManager : IAzureContainerConfigurationManager
    {
        private readonly AzureContainerOptions options;
        private readonly BlobServiceClient client;

        public AzureContainerConfigurationManager(IOptions<AzureContainerOptions> options, IAuthenticator authenticator)
        {
            if(authenticator == null)
            {
                throw new ArgumentNullException(nameof(authenticator));
            }

            this.client = authenticator.AuthenticateClient();
            this.options = options.Value;
        }

        public void Configure()
        {
            this.options.ContainersOptions.ToList().ForEach(option =>
            {
                var containerClient = this.client.GetBlobContainerClient(option.Name);

                containerClient.CreateIfNotExists();

                List<BlobSignedIdentifier> signedIdentifiers = new List<BlobSignedIdentifier>();

                option.ContainerPolicy.ToList().ForEach(policy =>
                {
                    signedIdentifiers.Add(new BlobSignedIdentifier
                    {
                        Id = policy.Identifier,
                        AccessPolicy = new BlobAccessPolicy
                        {
                            StartsOn = DateTimeOffset.UtcNow.AddHours(-1),
                            ExpiresOn = DateTimeOffset.UtcNow.AddDays(1),
                            Permissions = policy.Permissions
                        }
                    });
                });

                containerClient.SetAccessPolicy(permissions: signedIdentifiers);
            });
        }
    }
}