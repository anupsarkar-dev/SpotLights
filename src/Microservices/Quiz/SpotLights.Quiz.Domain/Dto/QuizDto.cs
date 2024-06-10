namespace SpotLights.Quiz.Domain.Dto;

public class QuizDto
{
  public int Id { get; set; }
  public int CourseId { get; set; }
  public string Title { get; set; } = default!;

  public ICollection<QuestionDto> Questions { get; set; } = default!;
}
