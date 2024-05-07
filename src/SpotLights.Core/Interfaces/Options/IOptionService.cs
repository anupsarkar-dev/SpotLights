namespace SpotLights.Core.Interfaces.Options
{
    public interface IOptionService
    {
        Task<bool> AnyKeyAsync(string key);
        Task<string?> GetByValueAsync(string key);
        Task SetValue(string key, string value);
    }
}
