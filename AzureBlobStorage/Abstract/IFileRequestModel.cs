namespace AzureBlobStorage.Abstract
{
    public interface IFileRequestModel : IRequestModel
    {
        public string UserRolePolicy { get; set; }
    }
}