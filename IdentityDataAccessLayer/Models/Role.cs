using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace IdentityDataAccessLayer.Models
{
    public class Role : IdentityRole<Guid>
    {
        public virtual ICollection<UserRole> Users { get; set; }
    }
}