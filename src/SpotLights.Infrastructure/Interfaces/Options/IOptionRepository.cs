namespace SpotLights.Infrastructure.Interfaces.Options
{
    public interface IOptionRepository : IBaseContextRepository
    {
        Task<bool> AnyKeyAsync(string key);
        Task<string?> GetByValueAsync(string key);
        Task SetValue(string key, string value);
    }
}