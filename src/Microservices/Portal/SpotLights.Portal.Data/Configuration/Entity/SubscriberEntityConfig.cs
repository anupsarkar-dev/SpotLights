using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpotLights.Common.Library.Base;
using SpotLights.Data.EntityConfiguration;
using SpotLights.Domain.Model.Newsletters;
using SpotLights.Domain.Model.Posts;

namespace SpotLights.Data.Configuration.Entity
{
  internal class SubscriberEntityConfig : IEntityTypeConfiguration<Subscriber>
  {
    public void Configure(EntityTypeBuilder<Subscriber> builder)
    {
      // Apply BaseEntity configuration
      BaseEntityConfig.ConfigureBaseEntity<Subscriber>(builder);
    }
  }
}
