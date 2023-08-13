namespace AzureBlobStorage.Abstract
{
    public interface IFilePolicy : IFileContainer
    {
        public string UserRolePolicy { get; set; }
    }
}