﻿using Abstract.Options;
using IdentityDataAccessLayer.Configuration;
using IdentityDataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;

namespace IdentityDataAccessLayer.Data
{
    public class IdentityContext 
        : IdentityDbContext<User, Role, Guid, IdentityUserClaim<Guid>, UserRole, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {
        private readonly DefaultConnectionStringsOptions options;

        public IdentityContext(IOptions<DefaultConnectionStringsOptions> options) 
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