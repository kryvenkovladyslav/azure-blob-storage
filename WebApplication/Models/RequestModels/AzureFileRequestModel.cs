using AzureBlobStorage.Abstract;

namespace WebApplication.Models.RequestModels
{
    public sealed class AzureFileRequestModel : IFilePolicy
    {
        public string UserRolePolicy { get; set; }

        public string ContainerName { get; set; }
    }
}