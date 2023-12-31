﻿using Azure.Storage.Blobs;
using AzureBlobStorage.Abstract;
using AzureBlobStorage.Options.ConfigurationOptions;
using Microsoft.Extensions.Options;

namespace AzureBlobStorage.Implementation
{
    public class AzureAccessKeyAuthenticator : IAuthenticator
    {
        private BlobServiceClient client;
        private readonly AzureAccessKeyOptions options;

        public AzureAccessKeyAuthenticator(IOptions<AzureAccessKeyOptions> options)
        {
            this.options = options.Value;
        }

        public BlobServiceClient AuthenticateClient()
        {
            if (this.client == null)
            {
                this.client = new BlobServiceClient(this.options.ConnectionString);
            }

            return this.client;
        }
    }
}