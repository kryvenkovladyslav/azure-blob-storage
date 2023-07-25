namespace AzureBlobStorage.Abstract
{
    public interface IRemoveRequestModel : IRequestModel
    {
        public string FileName { get; }
    }
}