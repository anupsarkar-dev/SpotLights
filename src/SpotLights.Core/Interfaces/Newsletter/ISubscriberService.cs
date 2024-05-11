using SpotLights.Core.Services;
using SpotLights.Shared;

namespace SpotLights.Core.Interfaces
{
    internal interface ISubscriberService : IBaseContexService
    {
        Task<int> ApplyAsync(SubscriberApplyDto input);
        Task<IEnumerable<SubscriberDto>> GetItemsAsync();
    }
}
