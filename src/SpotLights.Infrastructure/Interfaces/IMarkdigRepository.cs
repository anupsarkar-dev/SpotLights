namespace SpotLights.Infrastructure.Interfaces
{
    public interface IMarkdigRepository
    {
        string ToHtml(string markdown);
    }
}
