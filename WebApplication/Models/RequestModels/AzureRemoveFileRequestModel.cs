using AzureBlobStorage.Abstract;

namespace WebApplication.Models.RequestModels
{
    public sealed class AzureRemoveFileRequestModel : IRemoveRequestModel
    {
        public string FileName { get; set; }

        public string ContainerName { get; set; }
        public string UserRolePolicy { get; set; }
    }
}