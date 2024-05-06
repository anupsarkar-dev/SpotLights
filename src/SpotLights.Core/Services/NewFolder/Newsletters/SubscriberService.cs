using SpotLights.Core.Interfaces;
using SpotLights.Infrastructure.Interfaces;
using SpotLights.Shared;

namespace SpotLights.Infrastructure.Repositories.Newsletters;

public class SubscriberService : ISubscriberService
{
    private readonly ISubscriberRepository _subscriberRepository;

    public SubscriberService(ISubscriberRepository subscriberRepository)
    {
        _subscriberRepository = subscriberRepository;
    }

    public async Task<int> ApplyAsync(SubscriberApplyDto input)
    {
        return await _subscriberRepository.ApplyAsync(input);
    }

    public async Task<IEnumerable<SubscriberDto>> GetItemsAsync()
    {
        return await _subscriberRepository.GetItemsAsync();
    }
}
