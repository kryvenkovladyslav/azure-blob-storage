using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models.ViewModels
{
    public sealed class LoginViewModel
    {
        [Required]
        [MinLength(8)]
        [MaxLength(16)]
        public string UserName { get; set; }

        [Required]
        [MinLength(8)]
        [MaxLength(16)]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
    }
}