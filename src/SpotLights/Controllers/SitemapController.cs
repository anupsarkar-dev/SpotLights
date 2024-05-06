using SpotLights.Shared;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using SpotLights.Infrastructure.Repositories.Posts;

namespace SpotLights.Controllers;

public class SitemapController : ControllerBase
{
    private readonly PostRepository _postProvider;

    public SitemapController(PostRepository postProvider)
    {
        _postProvider = postProvider;
    }

    [Route("sitemap")]
    [Produces("text/xml")]
    public async Task<IActionResult> Sitemap()
    {
        XNamespace sitemapNamespace = XNamespace.Get("http://www.sitemaps.org/schemas/sitemap/0.9");
        System.Collections.Generic.IEnumerable<PostDto> posts = await _postProvider.GetAsync();
        XDocument doc =
            new(
                new XDeclaration("1.0", "utf-8", null),
                new XElement(
                    sitemapNamespace + "urlset",
                    from post in posts
                    select new XElement(
                        sitemapNamespace + "url",
                        new XElement(sitemapNamespace + "loc", GetPostUrl(post)),
                        new XElement(sitemapNamespace + "lastmod", GetPostDate(post)),
                        new XElement(sitemapNamespace + "changefreq", "monthly")
                    )
                )
            );
        return Content(doc.Declaration + Environment.NewLine + doc, "text/xml");
    }

    private string GetPostUrl(PostDto post)
    {
        string webRoot = Url.Content("~/");
        string sitemapBaseUri = $"{Request.Scheme}://{Request.Host}{webRoot}";
        return $"{sitemapBaseUri}posts/{post.Slug}";
    }

    private string GetPostDate(PostDto post)
    {
        return post.PublishedAt!.Value.ToString("yyyy-MM-ddTHH:mm:sszzz");
    }
}
