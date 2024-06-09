using SpotLights.Common.Library.Base;

namespace SpotLights.Quiz.Domain.Model;

public class Quiz : BaseEntity
{
  public int CourseId { get; set; }
  public string Title { get; set; } = default!;

  public ICollection<Question> Questions { get; set; } = default!;
}
