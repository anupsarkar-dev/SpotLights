using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpotLights.Quiz.Core.Interfaces;
using SpotLights.Quiz.Domain.Dto;

namespace SpotLights.Certification.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class QuizController : ControllerBase
{
  private readonly IQuizService _service;

  public QuizController(IQuizService service)
  {
    _service = service;
  }

  [HttpGet()]
  public async Task<IEnumerable<QuizDto>> GetAllAsync()
  {
    List<QuizDto> result = await _service
      .AsQuerable()
      .Include(s => s.Questions)
      .Select(s => s.Adapt<QuizDto>())
      .ToListAsync();

    return result;
  }

  [HttpGet("{id}")]
  public async Task<QuizDto?> GetByIdAsync(int id)
  {
    Quiz.Domain.Model.Quiz? result = await _service
      .AsQuerable()
      .Include(s => s.Questions)
      .SingleOrDefaultAsync(s => s.Id == id);

    return result.Adapt<QuizDto>();
  }

  [HttpPost()]
  public async Task<bool> AddAsync(QuizDto quiz)
  {
    Quiz.Domain.Model.Quiz item = quiz.Adapt<Quiz.Domain.Model.Quiz>();
    return await _service.AddAsync(item);
  }

  [HttpPut()]
  public async Task<bool> UpdateAsync(QuizDto Quiz)
  {
    Quiz.Domain.Model.Quiz item = Quiz.Adapt<Quiz.Domain.Model.Quiz>();
    return await _service.UpdateAsync(item);
  }

  [HttpDelete("{id}")]
  public async Task<bool> DeleteAsync(int id)
  {
    return await _service.DeleteAsync(id);
  }
}
