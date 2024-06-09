namespace SpotLights.Quiz.Domain.Model;

public class Question
{
  public int Id { get; set; }
  public int QuizId { get; set; }
  public string Text { get; set; } = default!;
  public string Options { get; set; } = default!;
  public bool IsCorrectOption { get; set; } = false;
  public Quiz Quiz { get; set; } = default!;
}
