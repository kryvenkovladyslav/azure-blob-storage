using AzureBlobStorage.Implementation;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AzureBlobStorage.Abstract
{
    public interface IAzureBlobStorageService
    {
        public Task<string> UploadAsync(IUploadRequestModel requestModel, CancellationToken token = default);

        public Task<IEnumerable<AzureFileInfo>> GetBlobsAsync(IFileRequestModel requestModel, CancellationToken token = default);

        public Task<bool> DeleteAsync(IRemoveRequestModel requestModel, CancellationToken token = default);
    }
}