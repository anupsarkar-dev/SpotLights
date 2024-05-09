using SpotLights.Core.Interfaces.Blogs;
using SpotLights.Infrastructure.Interfaces.Blogs;
using SpotLights.Shared;

namespace SpotLights.Core.Services.Blogs;

public class MainService : IMainService
{
    private readonly IMainRepository _mainRepository;

    public MainService(IMainRepository mainRepository)
    {
        _mainRepository = mainRepository;
    }

    public async Task<MainDto> GetAsync()
    {
        return await _mainRepository.GetAsync();
    }

    public async Task<List<CategoryItemDto>> GetCategoryItemesAsync()
    {
        return await _mainRepository.GetCategoryItemesAsync();
    }

    public async Task<List<CategoryItemDto>> GetCategoryItemesCacheAsync()
    {
        return await _mainRepository.GetCategoryItemesCacheAsync();
    }
}
