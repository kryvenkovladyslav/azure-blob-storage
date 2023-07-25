﻿using Azure.Storage.Blobs;
using AzureBlobStorage.Abstract;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Azure.Storage.Sas;
using Azure.Storage.Blobs.Models;

namespace AzureBlobStorage.Implementation
{
    public class AzureBlobStorageService : IAzureBlobStorageService
    {
        private readonly BlobServiceClient client;

        public AzureBlobStorageService(IAuthenticator authenticator)
        {
            if(authenticator == null)
            {
                throw new ArgumentNullException(nameof(authenticator));
            }

            this.client = authenticator.AuthenticateClient();
        }

        public virtual async Task<string> UploadAsync(IUploadRequestModel requestModel, CancellationToken token = default)
        {
            var containerClient = this.client.GetBlobContainerClient(requestModel.ContainerName);

            var blobClient = containerClient.GetBlobClient(requestModel.File.Name);

            using var context = requestModel.File.OpenReadStream();

            var uploadResult = await blobClient.UploadAsync(context, requestModel.OverrideExistingNamesFiles, token);

            return blobClient.Uri.OriginalString;
        }

        public virtual async Task<IEnumerable<string>> GetBlobsAsync(IFileRequestModel requestModel, CancellationToken token = default)
        {
            var fullyQualifiedUris = new List<string>();
            var containerClient = this.client.GetBlobContainerClient(requestModel.ContainerName);


            var sharedAccessSignature = containerClient.GenerateSasUri(new BlobSasBuilder
            {
                BlobContainerName = requestModel.ContainerName,
                Identifier = requestModel.UserRolePolicy
            });

            BlobClient blobClient;
            await foreach (var blob in containerClient.GetBlobsAsync(cancellationToken: token))
            {
                blobClient = containerClient.GetBlobClient(blob.Name);
                fullyQualifiedUris.Add(string.Concat(blobClient.Uri.OriginalString, sharedAccessSignature.Query));
            }

            return fullyQualifiedUris;
        }

        public virtual async Task<bool> DeleteAsync(IRemoveRequestModel requestModel, CancellationToken token = default)
        {
            var containerClient = this.client.GetBlobContainerClient(requestModel.ContainerName);

            return await containerClient.DeleteBlobIfExistsAsync(
                requestModel.FileName,
                DeleteSnapshotsOption.IncludeSnapshots,
                cancellationToken: token);
        }
    }
}