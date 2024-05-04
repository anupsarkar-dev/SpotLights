using System;
using System.ComponentModel.DataAnnotations;

namespace SpotLights.Domain;

public abstract class BaseEntity<TKey> where TKey : IEquatable<TKey>
{
  [Key]
  public virtual TKey Id { get; set; } = default!;
}
