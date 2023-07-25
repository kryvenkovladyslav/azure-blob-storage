using Azure.Storage.Blobs;

namespace AzureBlobStorage.Abstract
{
    public interface IAuthenticator
    {
        public BlobServiceClient AuthenticateClient();
    }
}