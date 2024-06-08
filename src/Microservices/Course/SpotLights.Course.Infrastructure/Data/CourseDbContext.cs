using Microsoft.EntityFrameworkCore;
using SpotLights.Course.Domain.Model;
using SpotLights.Course.Infrastructure.Data.EntityConfiguration.Course;
using SpotLights.Data.EntityConfiguration;

namespace SpotLights.Course.Infrastructure.Data
{
  public class CourseDbContext : DbContext
  {
    public CourseDbContext(DbContextOptions options)
      : base(options) { }

    public DbSet<Domain.Model.Course> Courses { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);

      builder.ApplyConfiguration(new BaseEntityConfig());

      builder.ApplyConfiguration(new CourseEntityConfig());
      builder.ApplyConfiguration(new EnrollmentEntityConfig());
    }
  }
}
