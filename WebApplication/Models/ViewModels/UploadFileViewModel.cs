using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models.ViewModels
{
    public sealed class UploadFileViewModel 
    {
        [Required]
        public IFormFile File { get; set; }

        public string RoleContainer { get; set; }
    }
}