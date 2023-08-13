using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System;
using WebApplication.Infrastructure.Common;

namespace WebApplication.Models.ViewModels
{
    public sealed class RegisterViewModel
    {
        public Guid? ID { get; set; }

        [Required(ErrorMessage = IdentityErrorMessages.requiredFirstName)]
        [DisplayName("Your first name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = IdentityErrorMessages.requiredLastName)]
        [DisplayName("Your last name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = IdentityErrorMessages.requiredUserName)]
        public string UserName { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = IdentityErrorMessages.requiredEmail)]
        public string Email { get; set; }

        [Required(ErrorMessage = IdentityErrorMessages.requiredPassword)]
        public string Password { get; set; }
    }
}