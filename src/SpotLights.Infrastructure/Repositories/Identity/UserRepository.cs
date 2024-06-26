using Mapster;
using Microsoft.EntityFrameworkCore;
using SpotLights.Data.Data;
using SpotLights.Domain.Model.Identity;
using SpotLights.Infrastructure.Interfaces.Identity;
using SpotLights.Shared;
using SpotLights.Shared.Entities.Identity;

namespace SpotLights.Infrastructure.Repositories.Identity;

internal class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _dbContext;

    public UserRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<UserInfo> FindAsync(int id)
    {
        IQueryable<UserInfo> query = _dbContext.Users.AsNoTracking().Where(m => m.Id == id);

        return await query.FirstAsync();
    }

    public async Task<UserDto> FirstByIdAsync(int id)
    {
        IQueryable<UserInfo> query = _dbContext.Users.AsNoTracking().Where(m => m.Id == id);

        return await query.ProjectToType<UserDto>().FirstAsync();
    }

    public async Task<IEnumerable<UserInfoDto>> GetAsync(bool isAdmin)
    {
        IQueryable<UserInfo> query = _dbContext.Users.AsNoTracking();

        query = ApplyIsAdminUserFilter(isAdmin, query);

        return await query.ProjectToType<UserInfoDto>().ToListAsync();
    }

    public async Task<UserInfoDto?> GetAsync(int id, bool isAdmin)
    {
        IQueryable<UserInfo> query = _dbContext.Users.AsNoTracking().Where(m => m.Id == id);

        query = ApplyIsAdminUserFilter(isAdmin, query);

        return await query.ProjectToType<UserInfoDto>().FirstOrDefaultAsync();
    }

    private static IQueryable<UserInfo> ApplyIsAdminUserFilter(
        bool isAdmin,
        IQueryable<UserInfo> query
    )
    {
        query = isAdmin ? query : query.Where(s => s.Type == UserType.Ordinary);
        return query;
    }
}
