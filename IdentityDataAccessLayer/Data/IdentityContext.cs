using Abstract.Models;
using Abstract.Options.ConnectionOptions;
using IdentityDataAccessLayer.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;

namespace IdentityDataAccessLayer.Data
{
    public class IdentityContext 
        : IdentityDbContext<ApplicationUser, ApplicationRole, Guid, IdentityUserClaim<Guid>, ApplicationUserRole, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {
        private readonly DefaultConnectionIdentityDatabaseOptions options;

        public IdentityContext(IOptions<DefaultConnectionIdentityDatabaseOptions> options) 
        {
            this.options = options.Value;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new UserRoleConfiguration());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(this.options.MsSqlServerConnection, sql => sql.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName));
        }
    }
}