using System;
using System.ComponentModel.DataAnnotations;

namespace SpotLights.Data;

public abstract class BaseEntity<TKey> where TKey : IEquatable<TKey>
{
  [Key]
  public virtual TKey Id { get; set; } = default!;
}
