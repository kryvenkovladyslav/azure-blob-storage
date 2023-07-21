using Microsoft.AspNetCore.Identity;
using System;

namespace Abstract.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set;}
    }
}