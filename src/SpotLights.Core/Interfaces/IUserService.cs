using SpotLights.Domain.Model.Identity;
using SpotLights.Shared;

namespace SpotLights.Core.Interfaces
{
    public interface IUserService
    {
        Task<UserInfo> FindAsync(int id);
        Task<UserDto> FirstByIdAsync(int id);
        Task<IEnumerable<UserInfoDto>> GetAsync(bool isAdmin);
        Task<UserInfoDto?> GetAsync(int id, bool isAdmin);
    }
}
