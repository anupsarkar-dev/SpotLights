namespace SpotLights.Course.Domain.Dto
{
  public class CourseDto
  {
    public int Id { get; set; }
    public string CourseName { get; set; } = default!;

    public string Description { get; set; } = default!;
    public int Credits { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public virtual ICollection<EnrollmentDto>? Enrollments { get; set; }
  }
}
