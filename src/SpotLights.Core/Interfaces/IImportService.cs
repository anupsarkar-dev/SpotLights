using SpotLights.Shared;
using SpotLights.Shared.Dtos;

namespace SpotLights.Core.Interfaces
{
    public interface IImportService
    {
        Task<IEnumerable<PostEditorDto>> WriteAsync(ImportDto request, int userId);
    }
}
