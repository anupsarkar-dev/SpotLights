namespace SpotLights.Infrastructure.Interfaces.Posts
{
    internal interface IMarkdigRepository
    {
        string ToHtml(string markdown);
    }
}
