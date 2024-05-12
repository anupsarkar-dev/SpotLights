using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SpotLights.Domain.Model.Identity;

namespace SpotLights.Infrastructure.Identity;

public class UserManager : UserManager<UserInfo>
{
    public UserManager(
        IUserStore<UserInfo> store,
        IOptions<IdentityOptions> optionsAccessor,
        IPasswordHasher<UserInfo> passwordHasher,
        IEnumerable<IUserValidator<UserInfo>> userValidators,
        IEnumerable<IPasswordValidator<UserInfo>> passwordValidators,
        ILookupNormalizer keyNormalizer,
        IdentityErrorDescriber errors,
        IServiceProvider services,
        ILogger<UserManager<UserInfo>> logger
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
        ) { }
}
