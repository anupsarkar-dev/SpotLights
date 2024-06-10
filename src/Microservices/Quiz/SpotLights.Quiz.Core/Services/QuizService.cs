using Microsoft.EntityFrameworkCore;
using SpotLights.Common.Library.Base;
using SpotLights.Common.Library.Interfaces;
using SpotLights.Quiz.Core.Interfaces;
using SpotLights.Quiz.Infrastructure.Interfaces;

namespace SpotLights.Course.Core.Services
{
  public class QuizService : Service<Quiz.Domain.Model.Quiz>, IQuizService
  {
    private readonly IQuizRepository _repo;

    public QuizService(IQuizRepository repository)
      : base(repository)
    {
      _repo = repository;
    }
  }
}
