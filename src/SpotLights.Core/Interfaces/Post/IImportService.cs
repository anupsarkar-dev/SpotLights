using SpotLights.Shared;
using SpotLights.Shared.Dtos;

namespace SpotLights.Core.Interfaces.Post
{
    internal interface IImportService
    {
        Task<IEnumerable<PostEditorDto>> WriteAsync(ImportDto request, int userId);
    }
}
