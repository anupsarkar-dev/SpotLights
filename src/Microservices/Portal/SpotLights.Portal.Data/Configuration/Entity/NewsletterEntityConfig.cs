using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpotLights.Common.Library.Base;
using SpotLights.Domain.Model.Newsletters;

namespace SpotLights.Data.Configuration.Entity
{
  internal class NewsletterEntityConfig : IEntityTypeConfiguration<Newsletter>
  {
    public void Configure(EntityTypeBuilder<Newsletter> builder)
    {
      // Apply BaseEntity configuration
      builder.HasBaseType<BaseEntity>();
    }
  }
}
