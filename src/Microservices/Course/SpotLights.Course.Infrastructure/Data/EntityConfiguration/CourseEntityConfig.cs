using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpotLights.Common.Library.Base;

namespace SpotLights.Data.Configuration.Entity
{
  internal class CourseEntityConfig
    : IEntityTypeConfiguration<SpotLights.Course.Domain.Model.Course>
  {
    public void Configure(EntityTypeBuilder<SpotLights.Course.Domain.Model.Course> builder)
    {
      // Apply BaseEntity configuration
      builder.HasBaseType<BaseEntity>();

      builder.ToTable(nameof(Course));
    }
  }
}
