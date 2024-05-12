using Microsoft.AspNetCore.Http;
using SpotLights.Shared;

namespace SpotLights.Core.Interfaces.Provider
{
    public interface IStorageProvider
    {
        Task<StorageDto?> UploadAsync(DateTime uploadAt, int userid, IFormFile file);

        Task<StorageDto> UploadAsync(
            DateTime uploadAt,
            int userid,
            Uri baseAddress,
            string url,
            string? fileName = null
        );

        Task<string> UploadFilesFoHtml(
            DateTime uploadAt,
            int userid,
            Uri baseAddress,
            string content
        );

        Task<string> UploadImagesBase64(DateTime uploadAt, int userid, string dataOrUrl);

        Task<string> UploadImagesBase64FoHtml(DateTime uploadAt, int userid, string content);

        Task<string> UploadImagesFoHtml(
            DateTime uploadAt,
            int userid,
            Uri baseAddress,
            string content
        );

        Task<string> UploadsFoHtmlAsync(
            DateTime uploadAt,
            int userid,
            Uri baseAddress,
            string content
        );
    }
}
