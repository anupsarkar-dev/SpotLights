using Mapster;
using Microsoft.AspNetCore.Mvc;
using SpotLights.Course.Core.Interfaces;
using SpotLights.Course.Domain.Dto;
using SpotLights.Course.Domain.Model;

namespace SpotLights.Course.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CourseController : ControllerBase
{
  private readonly ICourseService _service;

  public CourseController(ICourseService service)
  {
    _service = service;
  }

  [HttpGet()]
  public async Task<IEnumerable<CourseDto>> GetAllAsync()
  {
    IEnumerable<Domain.Model.Course> result = await _service.GetAllCoursesAsync();
    return result.Adapt<IEnumerable<CourseDto>>();
  }

  [HttpGet("{id}")]
  public async Task<CourseDto> GetByIdAsync(int id)
  {
    return (await _service.GetByIdAsync(id)).Adapt<CourseDto>();
  }

  [HttpPost()]
  public async Task<bool> AddAsync(CourseDto course)
  {
    var item = course.Adapt<Domain.Model.Course>();
    return await _service.AddAsync(item);
  }

  [HttpPost("Enrollment")]
  public async Task<bool> AddEnrollmentAsync(EnrollmentDto course)
  {
    var item = course.Adapt<Enrollment>();
    return await _service.AddEnrollment(item);
  }

  [HttpPut()]
  public async Task<bool> UpdateAsync(CourseDto course)
  {
    var item = course.Adapt<Domain.Model.Course>();
    return await _service.UpdateAsync(item);
  }

  [HttpDelete("{id}")]
  public async Task<bool> DeleteAsync(int id)
  {
    return await _service.DeleteAsync(id);
  }
}
