using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SpotLights.Domain.Model.Identity;
using SpotLights.Infrastructure.Interfaces.Identity;
using SpotLights.Infrastructure.Repositories.Identity;

namespace SpotLights.Core.Identity;

public class UsersManager : UserManager<UserInfo>
{
    protected readonly IUserRepository _userProvider;

    public UsersManager(
        IUserStore<UserInfo> store,
        IOptions<IdentityOptions> optionsAccessor,
        IPasswordHasher<UserInfo> passwordHasher,
        IEnumerable<IUserValidator<UserInfo>> userValidators,
        IEnumerable<IPasswordValidator<UserInfo>> passwordValidators,
        ILookupNormalizer keyNormalizer,
        IdentityErrorDescriber errors,
        IServiceProvider services,
        ILogger<UserManager<UserInfo>> logger,
        IUserRepository userProvider
    )
        : base(
            store,
            optionsAccessor,
            passwordHasher,
            userValidators,
            passwordValidators,
            keyNormalizer,
            errors,
            services,
            logger
        )
    {
        _userProvider = userProvider;
    }

    public Task<UserInfo> FindByIdAsync(int userId)
    {
        return _userProvider.FindAsync(userId);
    }
}
