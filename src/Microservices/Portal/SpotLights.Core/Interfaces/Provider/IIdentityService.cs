using SpotLights.Domain.Model.Identity;

namespace SpotLights.Core.Interfaces.Provider
{
    public interface IIdentityService
    {
        Task<UserInfo> FindByIdAsync(int userId);
    }
}
