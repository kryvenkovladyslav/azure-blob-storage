using Microsoft.AspNetCore.Identity;
using System;

namespace Abstract.Models
{
    public class ApplicationUserRole : IdentityUserRole<Guid>
    {
        public virtual ApplicationUser User { get; private set; }
        public virtual ApplicationRole Role { get; private set; }
    }
}