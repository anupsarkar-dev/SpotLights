using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpotLights.Common.Library.Base;
using SpotLights.Data.EntityConfiguration;

namespace SpotLights.Quiz.Infrastructure.Data.EntityConfiguration.Course
{
  internal class QuizEntityConfig : IEntityTypeConfiguration<Domain.Model.Quiz>
  {
    public void Configure(EntityTypeBuilder<Domain.Model.Quiz> builder)
    {
      // Apply BaseEntity configuration
      BaseEntityConfig.ConfigureBaseEntity<Domain.Model.Quiz>(builder);

      // Configure the relationship and cascade delete
      builder
        .HasMany(c => c.Questions)
        .WithOne(e => e.Quiz)
        .HasForeignKey(e => e.QuizId)
        .OnDelete(DeleteBehavior.Cascade);

      builder.ToTable(nameof(Course));
    }
  }
}
