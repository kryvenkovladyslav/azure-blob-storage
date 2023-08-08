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
            var containerClient = this.client.GetBlobContainerClient(requestModel.ContainerName);

            var sharedAccessSignature = this.GenerateSharedAccessSignature(containerClient, requestModel.ContainerName, requestModel.UserRolePolicy);
            var originlaUri = string.Concat(AzureBlobStorageAccountConstants.FullyQualifiedBlobUri, requestModel.ContainerName, "/", requestModel.File.Name);

            var secretUri = new Uri(string.Concat(originlaUri, sharedAccessSignature.Query));

            var blobClient = new BlobClient(secretUri);//containerClient.GetBlobClient(requestModel.File.Name);

            using var context = requestModel.File.Stream;

            var uploadResult = await blobClient.UploadAsync(context, requestModel.OverrideExistingNamesFiles, token);

            return blobClient.Uri.OriginalString;
        }

        public virtual async Task<IEnumerable<AzureFileInfo>> GetBlobsAsync(IFileRequestModel requestModel, CancellationToken token = default)
        {
            var files = new List<AzureFileInfo>();
            var containerClient = this.client.GetBlobContainerClient(requestModel.ContainerName);

            var sharedAccessSignature = this.GenerateSharedAccessSignature(containerClient, requestModel.ContainerName, requestModel.UserRolePolicy);

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
            var containerClient = this.client.GetBlobContainerClient(requestModel.ContainerName);

            var originlaUri = string.Concat(AzureBlobStorageAccountConstants.FullyQualifiedBlobUri, requestModel.ContainerName, "/", requestModel.FileName);
            var sharedAccessSignature = this.GenerateSharedAccessSignature(containerClient, requestModel.ContainerName, requestModel.UserRolePolicy);
            var secretUri = new Uri(string.Concat(originlaUri, sharedAccessSignature.Query));

            try
            {
                var blobClient = new BlobClient(secretUri);

                return await blobClient.DeleteIfExistsAsync(
                    DeleteSnapshotsOption.IncludeSnapshots,
                    cancellationToken: token);
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        private Uri GenerateSharedAccessSignature(BlobContainerClient client, string containerName, string identifier) 
        {
            return client.GenerateSasUri(new BlobSasBuilder
            {
                BlobContainerName = containerName,
                Identifier = identifier
            });
        }
    }
}