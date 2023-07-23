using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Abstract.Models
{
    public class ApplicationRole : IdentityRole<Guid>
    { 
        public virtual ICollection<ApplicationUserRole> Users{ get; set; }
    }
}