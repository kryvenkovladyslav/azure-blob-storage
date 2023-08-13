using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Abstract.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public virtual string FirstName { get; set; }

        public virtual string LastName { get; set; }

        public virtual ICollection<ApplicationUserRole> Roles { get; set; }
    }
}