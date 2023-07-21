using IdentityDataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityDataAccessLayer.Configuration
{
    public sealed class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasOne(ur => ur.Role).WithMany(r => r.Users)
               .HasForeignKey(ur => ur.RoleId).HasPrincipalKey(r => r.Id);

            builder.HasOne(ur => ur.User).WithMany(u => u.Roles)
                .HasForeignKey(ur => ur.UserId).HasPrincipalKey(u => u.Id);
        }
    }
}