using Markdig;
using SpotLights.Infrastructure.Interfaces;

namespace SpotLights.Infrastructure.Repositories.Posts;

public class MarkdigRepository : IMarkdigRepository, IMarkdigRepository1
{
    private readonly MarkdownPipeline _markdownPipeline;

    public MarkdigRepository()
    {
        _markdownPipeline = new MarkdownPipelineBuilder()
            .UsePipeTables()
            .UseAdvancedExtensions()
            .Build();
    }

    public string ToHtml(string markdown)
    {
        var html = Markdown.ToHtml(markdown, _markdownPipeline);
        //_logger.LogDebug("ToHtml markdown:{markdown}, html:{html}", markdown, html);
        return html;
    }
}
