namespace SpotLights.Course.Domain.Dto;

public record EnrollmentDto(int Id, int CourseId, int UserId, DateTime EnrollmentDate);
