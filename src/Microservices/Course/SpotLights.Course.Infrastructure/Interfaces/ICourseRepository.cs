using SpotLights.Common.Library.Interfaces;
using SpotLights.Course.Domain.Model;

namespace SpotLights.Course.Infrastructure.Interfaces
{
  public interface ICourseRepository : IRepository
  {
    Task<bool> AddEnrollment(Enrollment enrollment);
  }
}
