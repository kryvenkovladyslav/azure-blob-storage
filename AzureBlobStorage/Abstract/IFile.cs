using System.IO;

namespace AzureBlobStorage.Abstract
{
    public interface IFile
    {
        public string Name { get; }

        public Stream OpenReadStream();
    }
}