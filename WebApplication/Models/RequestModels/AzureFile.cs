using AzureBlobStorage.Abstract;
using System.IO;

namespace WebApplication.Models.RequestModels
{
    public sealed class AzureFile : IFile
    {
        public string Name { get; set; }

        public Stream Stream { get; set; }
    }
}