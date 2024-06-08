using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpotLights.Common.Library.Base;

namespace SpotLights.Course.Infrastructure.Data.EntityConfiguration.Course
{
  internal class CourseEntityConfig : IEntityTypeConfiguration<Domain.Model.Course>
  {
    public void Configure(EntityTypeBuilder<Domain.Model.Course> builder)
    {
      // Apply BaseEntity configuration
      builder.HasBaseType<BaseEntity>();

      builder.Property(c => c.CourseName).IsRequired().HasMaxLength(100);
      builder.Property(c => c.Description).HasMaxLength(500);

      // Configure the relationship and cascade delete
      builder
        .HasMany(c => c.Enrollments)
        .WithOne(e => e.Course)
        .HasForeignKey(e => e.CourseId)
        .OnDelete(DeleteBehavior.Cascade);

      builder.ToTable(nameof(Course));
    }
  }
}
