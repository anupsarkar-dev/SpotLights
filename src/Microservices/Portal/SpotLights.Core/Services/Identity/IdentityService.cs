using Microsoft.AspNetCore.Identity;
using SpotLights.Core.Interfaces.Provider;
using SpotLights.Domain.Model.Identity;
using SpotLights.Infrastructure.Interfaces.Identity;

namespace SpotLights.Core.Services.Identity
{
    internal class IdentityService : IIdentityService
    {
        private readonly IUserRepository _userRepository;

        public IdentityService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserInfo> FindByIdAsync(DefaultIdType userId)
        {
            return await _userRepository.FindAsync(userId);
        }
    }
}
