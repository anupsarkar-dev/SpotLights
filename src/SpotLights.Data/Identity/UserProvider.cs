using AutoMapper;
using SpotLights.Data;
using SpotLights.Shared;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpotLights.Shared.Identity;

namespace SpotLights.Data.Identity;

public class UserProvider
{
    private readonly IMapper _mapper;
    private readonly AppDbContext _dbContext;

    public UserProvider(IMapper mapper, AppDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public async Task<UserInfo> FindAsync(int id)
    {
        var query = _dbContext.Users.AsNoTracking().Where(m => m.Id == id);

        return await query.FirstAsync();
    }

    public async Task<UserDto> FirstByIdAsync(int id)
    {
        var query = _dbContext.Users.AsNoTracking().Where(m => m.Id == id);

        return await _mapper.ProjectTo<UserDto>(query).FirstAsync();
    }

    public async Task<IEnumerable<UserInfoDto>> GetAsync(bool isAdmin)
    {
        var query = _dbContext.Users.AsNoTracking();

        query = ApplyIsAdminUserFilter(isAdmin, (IQueryable<UserInfo>)query);

        return await _mapper.ProjectTo<UserInfoDto>(query).ToListAsync();
    }

    public async Task<UserInfoDto?> GetAsync(int id, bool isAdmin)
    {
        var query = _dbContext.Users.AsNoTracking().Where(m => m.Id == id);

        query = ApplyIsAdminUserFilter(isAdmin, (IQueryable<UserInfo>)query);

        return await _mapper.ProjectTo<UserInfoDto>(query).FirstOrDefaultAsync();
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
