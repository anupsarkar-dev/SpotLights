using SpotLights.Shared;

namespace SpotLights.Infrastructure.Interfaces
{
    public interface ISubscriberRepository
    {
        Task<int> ApplyAsync(SubscriberApplyDto input);
        Task<IEnumerable<SubscriberDto>> GetItemsAsync();
    }
}
