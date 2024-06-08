using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using SpotLights.Domain.Dto;

namespace SpotLights.Data.Configuration.Entity;

internal class OptionInfoEntityConfig : IEntityTypeConfiguration<OptionInfo>
{
    public void Configure(EntityTypeBuilder<OptionInfo> builder)
    {
        builder.ToTable("Options");
        builder.HasIndex(b => b.Key).IsUnique();
    }
}
