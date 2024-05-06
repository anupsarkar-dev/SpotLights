using SpotLights.Shared;

namespace SpotLights.Core.Interfaces
{
    public interface ISubscriberService
    {
        Task<int> ApplyAsync(SubscriberApplyDto input);
        Task<IEnumerable<SubscriberDto>> GetItemsAsync();
    }
}
