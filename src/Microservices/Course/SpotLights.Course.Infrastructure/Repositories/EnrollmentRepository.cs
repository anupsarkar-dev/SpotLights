using SpotLights.Common.Library.Base;
using SpotLights.Course.Infrastructure.Data;
using SpotLights.Course.Infrastructure.Interfaces;

namespace SpotLights.Course.Infrastructure.Repositories;

public class EnrollmentRepository : Repository<CourseDbContext>, IEnrollmentRepository
{
  public EnrollmentRepository(CourseDbContext context)
    : base(context) { }
}
