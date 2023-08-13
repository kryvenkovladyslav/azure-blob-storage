namespace AzureBlobStorage.Abstract
{
    public interface IRemoveRequestModel : IFileContainer, IFilePolicy
    {
        public string FileName { get; }
    }
}