using Microsoft.EntityFrameworkCore;
using SpotLights.Course.Domain.Model;

namespace SpotLights.Course.Infrastructure.Data
{
  public class CourseDbContext : DbContext
  {
    public CourseDbContext(DbContextOptions options)
      : base(options) { }

    public DbSet<Domain.Model.Course> Courses { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
  }
}
