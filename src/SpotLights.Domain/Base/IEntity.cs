namespace SpotLights.Domain.Base;

internal interface IEntity { }

internal interface IEntity<TId> : IEntity
{
    TId Id { get; set; }
}
