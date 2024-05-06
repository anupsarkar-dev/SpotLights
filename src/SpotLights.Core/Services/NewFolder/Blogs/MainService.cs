using SpotLights.Core.Interfaces;
using SpotLights.Infrastructure.Interfaces.Blogs;
using SpotLights.Shared;

namespace SpotLights.Infrastructure.Repositories.Blogs;

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
