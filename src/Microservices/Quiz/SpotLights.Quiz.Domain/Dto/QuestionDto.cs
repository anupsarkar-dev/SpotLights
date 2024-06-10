namespace SpotLights.Quiz.Domain.Dto
{
  public class QuestionDto
  {
    public int Id { get; set; }
    public int QuizId { get; set; }
    public string Text { get; set; } = default!;
    public string Options { get; set; } = default!;
    public bool IsCorrectOption { get; set; } = false;
  }
}
