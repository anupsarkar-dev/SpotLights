using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace SpotLights.Data.Configuration.Entity
{
    internal class IdentityUserEntityConfig : IEntityTypeConfiguration<IdentityUserClaim<int>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserClaim<int>> builder)
        {
            builder.ToTable("UserClaim");
            builder.Property(p => p.ClaimType).HasMaxLength(16);
            builder.Property(p => p.ClaimValue).HasMaxLength(256);
        }
    }
}
