namespace SpotLights.Infrastructure.Interfaces.Posts
{
    public interface IMarkdigRepository
    {
        string ToHtml(string markdown);
    }
}
