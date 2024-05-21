using System.ComponentModel.DataAnnotations;

namespace SpotLights.Domain.Base;

public abstract class BaseEntity : BaseEntity<DefaultIdType>
{
    protected BaseEntity() => Id = default;
}

public abstract class BaseEntity<TId> : IEntity<TId>
{
    public TId Id { get; set; } = default!;

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime? UpdatedAt { get; set; }

    [StringLength(128)]
    public string CreatedBy { get; set; } = default!;

    // For soft delete
    public bool IsDeleted { get; set; }
}
