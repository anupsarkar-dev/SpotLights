using SpotLights.Domain.Base;
using SpotLights.Domain.Model.Identity;
using SpotLights.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace SpotLights.Domain.Model.Posts;

public class Post : BaseEntity
{
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DefaultIdType UserId { get; set; }
    public UserInfo User { get; set; } = default!;

    [Required]
    [StringLength(160)]
    public string Title { get; set; } = default!;

    [Required]
    [StringLength(160)]
    public string Slug { get; set; } = default!;

    [Required]
    [StringLength(450)]
    public string Description { get; set; } = default!;

    public string Content { get; set; } = default!;

    [StringLength(160)]
    public string? Cover { get; set; }

    public int Views { get; set; }
    public DateTime? PublishedAt { get; set; }
    public PostType PostType { get; set; }
    public PostState State { get; set; }
    public List<PostCategory>? PostCategories { get; set; }
    //public List<StorageReference>? StorageReferences { get; set; }
}
