using SpotLights.Domain.Model.Identity;
using SpotLights.Infrastructure.Identity;

namespace SpotLights.Core.Provider
{
    internal class IdentityProvider
    {
        internal readonly UserManager _userManager;
        private readonly SignInManager _signInManager;

        public IdentityProvider(UserManager userManager, SignInManager signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        internal UserManager UserManager => _userManager;
        internal SignInManager SignInManager => _signInManager;

        public Task<UserInfo> FindByIdAsync(DefaultIdType userId)
        {
            return _userManager.FindByIdAsync(userId);
        }
    }
}
