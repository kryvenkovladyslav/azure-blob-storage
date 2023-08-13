using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Abstract.Models
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public ApplicationRole() { }

        public ApplicationRole(string roleName) : base(roleName) { }
       
        public virtual ICollection<ApplicationUserRole> Users{ get; set; }
    }
}