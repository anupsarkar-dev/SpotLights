using SpotLights.Common.Library.Base;
using SpotLights.Quiz.Infrastructure.Data;
using SpotLights.Quiz.Infrastructure.Interfaces;

namespace SpotLights.Quiz.Infrastructure.Repositories
{
  public class QuizRepository : Repository<QuizDbContext>, IQuizRepository
  {
    public QuizRepository(QuizDbContext context)
      : base(context) { }
  }
}
