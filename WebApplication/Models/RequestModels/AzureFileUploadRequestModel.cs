using AzureBlobStorage.Abstract;

namespace WebApplication.Models.RequestModels
{
    public sealed class AzureFileUploadRequestModel : IUploadRequestModel
    {
        public IFile File { get; set; }

        public string UserRolePolicy { get; set; }

        public string ContainerName { get; set; }

        public bool OverrideExistingNamesFiles { get; set; }
    }
}