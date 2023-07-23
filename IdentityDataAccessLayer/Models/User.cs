using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace IdentityDataAccessLayer.Models
{
    public class User : IdentityUser<Guid>
    {
        public virtual string FirstName { get; set; }

        public virtual string LastName { get; set; }

        public virtual ICollection<UserRole> Roles { get; set; }
    }
}