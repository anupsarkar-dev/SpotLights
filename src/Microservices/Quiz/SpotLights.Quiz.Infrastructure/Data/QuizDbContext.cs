using Microsoft.EntityFrameworkCore;
using SpotLights.Data.EntityConfiguration;
using SpotLights.Quiz.Domain.Model;
using SpotLights.Quiz.Infrastructure.Data.EntityConfiguration.Course;

namespace SpotLights.Quiz.Infrastructure.Data
{
  public class QuizDbContext : DbContext
  {
    public QuizDbContext(DbContextOptions options)
      : base(options) { }

    public DbSet<Domain.Model.Quiz> Courses { get; set; }
    public DbSet<Question> Enrollments { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);

      builder.ApplyConfiguration(new QuizEntityConfig());
      builder.ApplyConfiguration(new QuestionEntityConfig());
    }
  }
}
