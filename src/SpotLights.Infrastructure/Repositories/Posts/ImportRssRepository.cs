using SpotLights.Infrastructure.Interfaces.Posts;
using SpotLights.Shared;
using SpotLights.Shared.Dtos;
using SpotLights.Shared.Enums;
using SpotLights.Shared.Extensions;
using System.ServiceModel.Syndication;
using System.Xml;
using System.Xml.Linq;

namespace SpotLights.Infrastructure.Repositories.Posts;

internal class ImportRssRepository : IImportRssRepository
{
  public ImportDto Analysis(string feedUrl)
  {
    using XmlReader xml = XmlReader.Create(feedUrl);
    SyndicationFeed feed = SyndicationFeed.Load(xml);

    ImportDto result = new()
    {
      BaseUrl = feed.Id,
      Posts = [],
    };

    foreach (SyndicationItem? item in feed.Items)
    {
      string content = ((TextSyndicationContent)item.Content).Text;
      PostEditorDto post = new()
      {
        Slug = item.Id,
        Title = item.Title.Text,
        Description = GetDescription(item.Summary.Text),
        Content = content,
        PublishedAt = item.PublishDate.DateTime,
        PostType = PostType.Post,
      };

      if (item.ElementExtensions != null)
      {
        foreach (SyndicationElementExtension ext in item.ElementExtensions)
        {
          if (ext.GetObject<XElement>().Name.LocalName == "summary")
          {
            post.Description = GetDescription(ext.GetObject<XElement>().Value);
          }

          if (ext.GetObject<XElement>().Name.LocalName == "cover")
          {
            post.Cover = ext.GetObject<XElement>().Value;
          }
        }
      }

      if (item.Categories != null)
      {
        post.Categories ??= [];
        foreach (SyndicationCategory? category in item.Categories)
        {
          post.Categories.Add(new CategoryDto
          {
            Content = category.Name
          });
        }
      }
      result.Posts.Add(post);
    }
    return result;
  }

  private static string GetDescription(string description)
  {
    description = description.StripHtml();
    if (description.Length > 450)
    {
      description = description[..446] + "...";
    }

    return description;
  }
}
