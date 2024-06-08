using Markdig;
using SpotLights.Infrastructure.Interfaces.Posts;

namespace SpotLights.Infrastructure.Repositories.Posts;

internal class MarkdigRepository : IMarkdigRepository
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
