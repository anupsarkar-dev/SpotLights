using System.ComponentModel.DataAnnotations;

namespace SpotLights.Common.Library.Base;

public abstract class BaseEntity : BaseEntity<int>
{
  protected BaseEntity() => Id = default;
}

public abstract class BaseEntity<TId> : IEntity<TId>
{
  public TId Id { get; set; } = default!;

  public DateTime CreatedAt { get; set; }

  public DateTime? UpdatedAt { get; set; }

  public string? CreatedBy { get; set; }

  // For soft delete
  public bool IsDeleted { get; set; }
}
