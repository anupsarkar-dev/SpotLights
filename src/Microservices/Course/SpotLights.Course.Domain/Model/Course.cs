using System.ComponentModel.DataAnnotations;
using SpotLights.Common.Library.Base;

namespace SpotLights.Course.Domain.Model;

public class Course : BaseEntity
{
  [MaxLength(200)]
  public string CourseName { get; set; } = default!;

  [MaxLength(5000)]
  public string Description { get; set; } = default!;
  public int Credits { get; set; }
  public DateTime StartDate { get; set; }
  public DateTime EndDate { get; set; }

  public virtual ICollection<Enrollment> Enrollments { get; set; } = default!;
}
