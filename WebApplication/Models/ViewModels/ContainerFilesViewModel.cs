using System.Collections.Generic;

namespace WebApplication.Models.ViewModels
{
    public sealed class ContainerFilesViewModel
    {
        public string ContainerName { get; set; }

        public IEnumerable<FileViewModel> Files { get; set; }
    }
}