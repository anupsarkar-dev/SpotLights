namespace SpotLights.Core.Interfaces
{
    public interface IOptionService
    {
        Task<bool> AnyKeyAsync(string key);
        Task<string?> GetByValueAsync(string key);
        Task SetValue(string key, string value);
    }
}
