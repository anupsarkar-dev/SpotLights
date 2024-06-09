using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpotLights.Quiz.Domain.Model;

namespace SpotLights.Quiz.Infrastructure.Data.EntityConfiguration.Course
{
  internal class QuestionEntityConfig : IEntityTypeConfiguration<Question>
  {
    public void Configure(EntityTypeBuilder<Question> builder)
    {
      builder.Property(e => e.Text).IsRequired();

      builder.ToTable(nameof(Question));
    }
  }
}
