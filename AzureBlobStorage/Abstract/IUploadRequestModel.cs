namespace AzureBlobStorage.Abstract
{
    public interface IUploadRequestModel : IFileContainer, IFilePolicy
    {
        public IFile File { get; }

        public bool OverrideExistingNamesFiles { get; }
    }
}