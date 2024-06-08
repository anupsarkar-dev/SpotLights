using SpotLights.Shared;

namespace SpotLights.Infrastructure.Interfaces
{
    internal interface ISubscriberRepository : IBaseContextRepository
    {
        Task<int> ApplyAsync(SubscriberApplyDto input);
        Task<IEnumerable<SubscriberDto>> GetItemsAsync();
    }
}
