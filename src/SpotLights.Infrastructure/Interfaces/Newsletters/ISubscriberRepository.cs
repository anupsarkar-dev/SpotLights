using SpotLights.Shared;

namespace SpotLights.Infrastructure.Interfaces
{
    public interface ISubscriberRepository : IBaseContextRepository
    {
        Task<int> ApplyAsync(SubscriberApplyDto input);
        Task<IEnumerable<SubscriberDto>> GetItemsAsync();
    }
}
