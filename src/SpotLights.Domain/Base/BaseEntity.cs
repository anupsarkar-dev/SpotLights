namespace SpotLights.Domain.Base;

public abstract class BaseEntity : BaseEntity<DefaultIdType>
{
    protected BaseEntity() => Id = default;
}

public abstract class BaseEntity<TId> : IEntity<TId>
{
    public TId Id { get; set; } = default!;
}
