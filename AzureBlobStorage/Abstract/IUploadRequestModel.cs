namespace AzureBlobStorage.Abstract
{
    public interface IUploadRequestModel : IRequestModel
    {
        public IFile File { get; }

        public bool OverrideExistingNamesFiles { get; }

        public string UserRolePolicy { get; }
    }
}