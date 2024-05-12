using SpotLights.Core.Interfaces;
using SpotLights.Core.Services;
using SpotLights.Infrastructure.Interfaces;
using SpotLights.Shared;

namespace SpotLights.Infrastructure.Repositories.Newsletters;

internal class SubscriberService : BaseContextService, ISubscriberService
{
    private readonly ISubscriberRepository _subscriberRepository;

    public SubscriberService(ISubscriberRepository subscriberRepository)
        : base(subscriberRepository)
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
