using SpotLights.Core.Interfaces.Options;
using SpotLights.Infrastructure.Interfaces.Options;

namespace SpotLights.Core.Services.Options;

internal class OptionService : IOptionService
{
    private readonly IOptionRepository _optionRepository;

    public OptionService(IOptionRepository optionRepository)
    {
        _optionRepository = optionRepository;
    }

    public async Task<bool> AnyKeyAsync(string key)
    {
        return await _optionRepository.AnyKeyAsync(key);
    }

    public async Task<string?> GetByValueAsync(string key)
    {
        return await _optionRepository.GetByValueAsync(key);
    }

    public async Task SetValue(string key, string value)
    {
        await _optionRepository.SetValue(key, value);
    }
}
