namespace AzureBlobStorage.Abstract
{
    public interface IRemoveRequestModel : IRequestModel, IFileRequestModel
    {
        public string FileName { get; }
    }
}