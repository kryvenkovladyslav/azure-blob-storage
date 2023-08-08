using Microsoft.AspNetCore.Http;

namespace WebApplication.Models.ViewModels
{
    public sealed class UploadFileViewModel 
    {
        public IFormFile File { get; set; }

        public string RoleContainer { get; set; }
    }
}