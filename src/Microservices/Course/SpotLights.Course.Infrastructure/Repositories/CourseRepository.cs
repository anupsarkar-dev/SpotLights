using SpotLights.Common.Library.Base;
using SpotLights.Course.Domain.Dto;
using SpotLights.Course.Domain.Model;
using SpotLights.Course.Infrastructure.Data;
using SpotLights.Course.Infrastructure.Interfaces;

namespace SpotLights.Course.Infrastructure.Repositories;

public class CourseRepository : Repository<CourseDbContext>, ICourseRepository
{
  public CourseRepository(CourseDbContext context)
    : base(context) { }

  public async Task<bool> AddEnrollment(Enrollment enrollment)
  {
    await _context.AddAsync(enrollment);
    return await SaveChangesAsync();
  }
}
