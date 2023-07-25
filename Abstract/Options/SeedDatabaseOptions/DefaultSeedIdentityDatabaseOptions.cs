using System.Collections.Generic;

namespace Abstract.Options.SeedDatabaseOptions
{
    public sealed class DefaultSeedIdentityDatabaseOptions
    {
        public static string Position { get; } = nameof(DefaultSeedIdentityDatabaseOptions);

        public ICollection<DefaultSeedUserOptions> Users { get; set; }
    }
}