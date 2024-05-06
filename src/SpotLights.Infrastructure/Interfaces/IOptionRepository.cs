namespace SpotLights.Infrastructure.Interfaces
{
    public interface IOptionRepository
    {
        Task<bool> AnyKeyAsync(string key);
        Task<string?> GetByValueAsync(string key);
        Task SetValue(string key, string value);
    }
}
