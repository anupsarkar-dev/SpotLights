using System.ComponentModel.DataAnnotations;

namespace SpotLights.Shared;

public class ImportRssDto
{
  [Required][Url] public string FeedUrl { get; set; } = default!;
}
