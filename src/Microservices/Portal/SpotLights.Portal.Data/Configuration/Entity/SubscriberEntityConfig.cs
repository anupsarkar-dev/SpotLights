using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpotLights.Common.Library.Base;
using SpotLights.Domain.Model.Newsletters;

namespace SpotLights.Data.Configuration.Entity
{
  internal class SubscriberEntityConfig : IEntityTypeConfiguration<Subscriber>
  {
    public void Configure(EntityTypeBuilder<Subscriber> builder)
    {
      // Apply BaseEntity configuration
      builder.HasBaseType<BaseEntity>();
    }
  }
}
