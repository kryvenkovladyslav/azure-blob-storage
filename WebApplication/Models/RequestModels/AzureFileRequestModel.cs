using AzureBlobStorage.Abstract;

namespace WebApplication.Models.RequestModels
{
    public sealed class AzureFileRequestModel : IFileRequestModel
    {
        public string UserRolePolicy { get; set; }

        public string ContainerName { get; set; }
    }
}