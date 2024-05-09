using SpotLights.Shared;
using SpotLights.Shared.Dtos;

namespace SpotLights.Infrastructure.Interfaces.Posts
{
    public interface IImportRepository
    {
        Task<IEnumerable<PostEditorDto>> WriteAsync(ImportDto request, int userId);
    }
}
