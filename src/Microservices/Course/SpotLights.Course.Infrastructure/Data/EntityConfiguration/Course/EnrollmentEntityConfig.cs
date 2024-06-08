using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpotLights.Common.Library.Base;
using SpotLights.Course.Domain.Model;

namespace SpotLights.Course.Infrastructure.Data.EntityConfiguration.Course
{
  internal class EnrollmentEntityConfig : IEntityTypeConfiguration<Enrollment>
  {
    public void Configure(EntityTypeBuilder<Enrollment> builder)
    {
      builder.Property(e => e.EnrollmentDate).IsRequired();

      // Configure the relationship and cascade delete (redundant here since it's already configured in CourseConfiguration)
      builder
        .HasOne(e => e.Course)
        .WithMany(c => c.Enrollments)
        .HasForeignKey(e => e.CourseId)
        .OnDelete(DeleteBehavior.Cascade);

      builder.ToTable(nameof(Enrollment));
    }
  }
}
