using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SpotLights.Domain.Model.Newsletters;
using SpotLights.Domain.Base;

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
