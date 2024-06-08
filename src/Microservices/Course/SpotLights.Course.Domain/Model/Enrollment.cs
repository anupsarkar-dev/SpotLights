namespace SpotLights.Course.Domain.Model;

public class Enrollment
{
  public int CourseId { get; set; }
  public int UserId { get; set; }
  public DateTime EnrollmentDate { get; set; }
  public virtual Course Course { get; set; } = default!;
}
