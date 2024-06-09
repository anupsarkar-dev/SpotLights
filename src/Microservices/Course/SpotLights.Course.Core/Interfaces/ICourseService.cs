using SpotLights.Common.Library.Interfaces;
using SpotLights.Course.Domain.Model;

namespace SpotLights.Course.Core.Interfaces
{
  public interface ICourseService : IService<Domain.Model.Course>
  {
    Task<bool> AddEnrollment(Enrollment enrollment);
    Task<IEnumerable<Domain.Model.Course>> GetAllCoursesAsync();
  }
}
