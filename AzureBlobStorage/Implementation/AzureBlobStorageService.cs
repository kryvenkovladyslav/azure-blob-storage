using Azure.Storage.Blobs;
using AzureBlobStorage.Abstract;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Azure.Storage.Sas;
using Azure.Storage.Blobs.Models;
using AzureBlobStorage.Common;

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
            var secretUri = this.GenerateSecretUri(requestModel.ContainerName, requestModel.File.Name, requestModel.UserRolePolicy);

            var blobClient = this.GetBlobClient(secretUri);

            using var context = requestModel.File.Stream;

            var uploadResult = await blobClient.UploadAsync(context, requestModel.OverrideExistingNamesFiles, token);

            return blobClient.Uri.OriginalString;
        }

        public virtual async Task<IEnumerable<AzureFileInfo>> GetBlobsAsync(IFilePolicy requestModel, CancellationToken token = default)
        {
            var files = new List<AzureFileInfo>();

            var containerClient = this.client.GetBlobContainerClient(requestModel.ContainerName);

            var sharedAccessSignature = this.GenerateSharedAccessSignature(requestModel.ContainerName, requestModel.UserRolePolicy);

            BlobClient blobClient;
            await foreach (var blob in containerClient.GetBlobsAsync(cancellationToken: token))
            {
                blobClient = containerClient.GetBlobClient(blob.Name);
                files.Add(new AzureFileInfo(string.Concat(blobClient.Uri.OriginalString, sharedAccessSignature.Query), blobClient.Name));
            }

            return files;
        }

        public virtual async Task<bool> DeleteAsync(IRemoveRequestModel requestModel, CancellationToken token = default)
        {
            var secretUri = this.GenerateSecretUri(requestModel.ContainerName, requestModel.FileName, requestModel.UserRolePolicy);
            
            var blobClient = this.GetBlobClient(secretUri);
            
            return await blobClient.DeleteIfExistsAsync(
                DeleteSnapshotsOption.IncludeSnapshots,
                cancellationToken: token);
        }

        private BlobClient GetBlobClient(Uri secret)
        {
            return new BlobClient(secret);
        }

        private Uri GenerateSharedAccessSignature(string containerName, string identifier) 
        {
            var containerClient = this.client.GetBlobContainerClient(containerName);

            return containerClient.GenerateSasUri(new BlobSasBuilder
            {
                BlobContainerName = containerName,
                Identifier = identifier
            });
        }

        private Uri GenerateSecretUri(string containerName, string fileName, string userPolicy)
        {
            var originalUri = string.Concat(AzureBlobStorageAccountConstants.FullyQualifiedBlobUri, containerName, "/", fileName);
            return new Uri(string.Concat(originalUri, this.GenerateSharedAccessSignature(containerName, userPolicy).Query));
        }
    }
}