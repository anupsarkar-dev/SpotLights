using Microsoft.EntityFrameworkCore;
using SpotLights.Common.Library.Base;
using SpotLights.Common.Library.Interfaces;
using SpotLights.Course.Core.Interfaces;
using SpotLights.Course.Domain.Dto;
using SpotLights.Course.Domain.Model;
using SpotLights.Course.Infrastructure.Interfaces;

namespace SpotLights.Course.Core.Services
{
  public class CourseService : Service<Domain.Model.Course>, ICourseService
  {
    private readonly ICourseRepository _courseRepository;

    public CourseService(ICourseRepository courseRepository)
      : base(courseRepository)
    {
      _courseRepository = courseRepository;
    }

    public async Task<IEnumerable<Domain.Model.Course>> GetAllCoursesAsync()
    {
      return await _courseRepository
        .AsQuerable<Domain.Model.Course>()
        .Include(s => s.Enrollments)
        .ToListAsync();
    }

    public async Task<bool> AddEnrollment(Enrollment enrollment)
    {
      return await _courseRepository.AddEnrollment(enrollment);
    }
  }
}
