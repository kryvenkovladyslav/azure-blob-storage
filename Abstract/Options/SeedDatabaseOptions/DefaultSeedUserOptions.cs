﻿using Abstract.Common;
using System.Collections.Generic;

namespace Abstract.Options.SeedDatabaseOptions
{
    public sealed class DefaultSeedUserOptions
    {
        public static string Position { get; } = nameof(DefaultSeedUserOptions);

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public ICollection<ApplicationRoles> Roles { get; set; }
    }
}