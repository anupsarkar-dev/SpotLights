using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SpotLights.Domain.Model.Identity;

namespace SpotLights.Data.Configuration.Entity
{
    internal class UserEntityConfig : IEntityTypeConfiguration<UserInfo>
    {
        public void Configure(EntityTypeBuilder<UserInfo> builder)
        {
            builder.ToTable("Users");
            builder.Property(p => p.Id).HasMaxLength(128);

            builder.Property(p => p.PasswordHash).HasMaxLength(256);
            builder.Property(p => p.SecurityStamp).HasMaxLength(32);
            builder.Property(p => p.ConcurrencyStamp).HasMaxLength(64);
            builder.Property(p => p.PhoneNumber).HasMaxLength(32);
            builder.Property(p => p.CreatedAt);
            builder.Property(p => p.UpdatedAt);
            builder.Property(p => p.CreatedBy).HasMaxLength(128).IsRequired(false);
        }
    }
}
