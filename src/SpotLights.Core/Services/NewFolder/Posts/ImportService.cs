using SpotLights.Core.Interfaces;
using SpotLights.Infrastructure.Interfaces.Posts;
using SpotLights.Shared;
using SpotLights.Shared.Dtos;

namespace SpotLights.Infrastructure.Repositories.Posts;

public class ImportService : IImportService
{
    private readonly IImportRepository _importRepository;

    public ImportService(IImportRepository importRepository)
    {
        _importRepository = importRepository;
    }

    public async Task<IEnumerable<PostEditorDto>> WriteAsync(ImportDto request, int userId)
    {
        return await _importRepository.WriteAsync(request, userId);
    }
}
