using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SpotLights.Domain.Model.Posts;
using SpotLights.Domain.Model.Newsletters;
using SpotLights.Domain.Base;

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
