using SpotLights.Shared;
using SpotLights.Shared.Dtos;

namespace SpotLights.Infrastructure.Interfaces
{
    public interface IImportRepository
    {
        Task<IEnumerable<PostEditorDto>> WriteAsync(ImportDto request, int userId);
    }
}
