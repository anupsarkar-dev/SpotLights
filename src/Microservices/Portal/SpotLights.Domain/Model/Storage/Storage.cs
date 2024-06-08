using System.ComponentModel.DataAnnotations;
using SpotLights.Common.Library.Base;
using SpotLights.Domain.Model.Identity;
using SpotLights.Shared.Enums;

namespace SpotLights.Domain.Model.Storage;

public class Storage : BaseEntity
{
  public DefaultIdType UserId { get; set; }
  public UserInfo User { get; set; } = default!;

  [StringLength(2048)]
  public string Slug { get; set; } = default!;

  [StringLength(1024)]
  public string Name { get; set; } = default!;

  [StringLength(2048)]
  public string Path { get; set; } = default!;

  public long Length { get; set; }

  [StringLength(128)]
  public string ContentType { get; set; } = default!;

  public StorageType Type { get; set; }

  //public List<StorageReference>? StorageReferences { get; set; }

  public DateTime UploadAt { get; set; }
}
