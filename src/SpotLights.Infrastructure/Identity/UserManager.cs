using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SpotLights.Domain.Model.Identity;
using SpotLights.Infrastructure.Repositories.Identity;

namespace SpotLights.Infrastructure.Identity;

public class UserManager : UserManager<UserInfo>
{
    protected readonly UserRepository _userProvider;

    public UserManager(
        IUserStore<UserInfo> store,
        IOptions<IdentityOptions> optionsAccessor,
        IPasswordHasher<UserInfo> passwordHasher,
        IEnumerable<IUserValidator<UserInfo>> userValidators,
        IEnumerable<IPasswordValidator<UserInfo>> passwordValidators,
        ILookupNormalizer keyNormalizer,
        IdentityErrorDescriber errors,
        IServiceProvider services,
        ILogger<UserManager<UserInfo>> logger,
        UserRepository userProvider
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
