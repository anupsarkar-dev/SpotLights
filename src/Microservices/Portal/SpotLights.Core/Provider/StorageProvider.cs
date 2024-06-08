using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SpotLights.Core.Interfaces.Provider;
using SpotLights.Helper;
using SpotLights.Infrastructure.Interfaces.Storages;
using SpotLights.Shared;
using SpotLights.Shared.Constants;
using SpotLights.Shared.Extensions;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace SpotLights.Core.Provider;

internal class StorageProvider : IStorageProvider
{
    private readonly ILogger _logger;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IStorageMinioProvider _storageProvider;
    private readonly string[]? _fileExtensions;
    private readonly IContentTypeProvider _contentTypeProvider;

    public StorageProvider(
        ILogger<StorageProvider> logger,
        IHttpClientFactory httpClientFactory,
        IStorageMinioProvider storageProvider,
        IConfiguration configuration,
        IContentTypeProvider contentTypeProvider
    )
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
        _storageProvider = storageProvider;
        _contentTypeProvider = contentTypeProvider;

        string? fileExtensionsString = configuration[$"{SpotLightsConstant.Key}:FileExtensions"];
        if (!string.IsNullOrEmpty(fileExtensionsString))
        {
            _fileExtensions = fileExtensionsString.Split(',')!;
        }
    }

    public async Task<StorageDto> UploadAsync(
        DateTime uploadAt,
        int userid,
        Uri baseAddress,
        string url,
        string? fileName = null
    )
    {
        using HttpClient client = _httpClientFactory.CreateClient();
        client.BaseAddress = baseAddress;
        using HttpResponseMessage response = await client.GetAsync(url);
        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException("url not content");
        }

        string folder = $"{userid}/{uploadAt.Year}{uploadAt.Month}";
        string? path = null;
        if (fileName == null)
        {
            fileName = response.Content.Headers.ContentDisposition?.FileNameStar;
            if (!string.IsNullOrEmpty(fileName))
            {
                path = $"{folder}/{fileName}";
            }
            else
            {
                fileName = GetFileNameByUrl(url);
                path = $"{folder}/{fileName}";
            }
        }
        else
        {
            path = $"{folder}/{fileName}";
        }
        StorageDto? storage = await _storageProvider.GetCheckStoragAsync(path);
        if (storage != null)
        {
            return storage;
        }

        using Stream stream = await response.Content.ReadAsStreamAsync();
        string? contentType = response.Content.Headers.ContentType?.ToString();
        storage = await _storageProvider.AddAsync(
            uploadAt,
            userid,
            path,
            fileName,
            stream,
            contentType!
        );
        return storage;
    }

    public async Task<StorageDto?> UploadAsync(DateTime uploadAt, int userid, IFormFile file)
    {
        string fileName = GetFileName(file.FileName);
        if (!InvalidFileName(fileName))
        {
            _logger.LogError("Invalid file name: {fileName}", fileName);
            return null;
        }

        string path = $"{userid}/{uploadAt.Year}{uploadAt.Month}/{fileName}";
        StorageDto? storage = await _storageProvider.GetCheckStoragAsync(path);
        if (storage != null)
        {
            return storage;
        }

        Stream stream = file.OpenReadStream();
        storage = await _storageProvider.AddAsync(
            uploadAt,
            userid,
            path,
            fileName,
            stream,
            file.ContentType
        );
        return storage;
    }

    public async Task<string> UploadsFoHtmlAsync(
        DateTime uploadAt,
        int userid,
        Uri baseAddress,
        string content
    )
    {
        string uploadeImageContent = await UploadImagesFoHtml(
            uploadAt,
            userid,
            baseAddress,
            content
        );
        string uploadeFileContent = await UploadFilesFoHtml(
            uploadAt,
            userid,
            baseAddress,
            uploadeImageContent
        );
        return uploadeFileContent;
    }

    public async Task<string> UploadImagesFoHtml(
        DateTime uploadAt,
        int userid,
        Uri baseAddress,
        string content
    )
    {
        MatchCollection matches = StringHelper.HtmlImgTagsGeneratedRegex().Matches(content);
        if (matches.Any())
        {
            StringBuilder contentBuilder = new(content);
            Regex htmlImgSrcRegex = StringHelper.HtmlImgSrcGeneratedRegex();
            foreach (Match match in matches.Cast<Match>())
            {
                string tag = match.Value;
                Match matchUrl = htmlImgSrcRegex.Match(tag);
                string urlString = matchUrl.Groups[1].Value;
                StorageDto storage = await UploadAsync(uploadAt, userid, baseAddress, urlString);
                string uploadTag = $"![{storage.Name}]({storage.Slug})";
                _ = contentBuilder.Replace(tag, uploadTag);
            }
            content = contentBuilder.ToString();
        }
        return content;
    }

    public async Task<string> UploadFilesFoHtml(
        DateTime uploadAt,
        int userid,
        Uri baseAddress,
        string content
    )
    {
        MatchCollection matches = StringHelper.HtmlFileGeneratedRegex().Matches(content);
        if (matches.Any())
        {
            StringBuilder contentBuilder = new(content);
            foreach (Match match in matches.Cast<Match>())
            {
                string tag = match.Value;
                string urlString = XElement.Parse(tag).Attribute("href")!.Value;
                if (InvalidFileName(urlString))
                {
                    StorageDto storage = await UploadAsync(
                        uploadAt,
                        userid,
                        baseAddress,
                        urlString
                    );
                    string uploadTag = $"![{storage.Name}]({storage.Slug})";
                    _ = contentBuilder.Replace(tag, uploadTag);
                }
            }
            content = contentBuilder.ToString();
        }
        return content;
    }

    public async Task<string> UploadImagesBase64FoHtml(
        DateTime uploadAt,
        int userid,
        string content
    )
    {
        MatchCollection dataImageBase64Matches = StringHelper
            .MarkdownDataImageBase64BlobGeneratedRegex()
            .Matches(content);
        if (dataImageBase64Matches.Count > 0)
        {
            StringBuilder contentStringBuilder = new(content);
            foreach (Match match in dataImageBase64Matches.Cast<Match>())
            {
                string fileNameMarkdown = match.Groups["filename"].Value;
                string imageType = match.Groups["type"].Value;
                string base64Data = match.Groups["data"].Value;
                byte[] imageDataBytes = Convert.FromBase64String(base64Data);
                string fileName = Guid.NewGuid().ToString() + "." + imageType;
                string path = $"{userid}/{uploadAt.Year}{uploadAt.Month}/{fileName}";
                if (!_contentTypeProvider.TryGetContentType(fileName, out string? contentType))
                {
                    contentType = "text/html";
                }

                StorageDto storage = await _storageProvider.AddAsync(
                    uploadAt,
                    userid,
                    path,
                    fileName,
                    imageDataBytes,
                    contentType
                );
                string uploadTag = $"![{fileNameMarkdown}]({storage.Slug})";
                _ = contentStringBuilder.Replace(match.Value, uploadTag);
            }
            content = contentStringBuilder.ToString();
        }
        return content;
    }

    public async Task<string> UploadImagesBase64(DateTime uploadAt, int userid, string dataOrUrl)
    {
        Match match = StringHelper.DataImageBase64GeneratedRegex().Match(dataOrUrl);
        if (match.Success)
        {
            string imageType = match.Groups["type"].Value;
            string base64Data = match.Groups["data"].Value;
            byte[] imageDataBytes = Convert.FromBase64String(base64Data);
            string fileName = Guid.NewGuid().ToString() + "." + imageType;
            string path = $"{userid}/{uploadAt.Year}{uploadAt.Month}/{fileName}";
            if (!_contentTypeProvider.TryGetContentType(fileName, out string? contentType))
            {
                contentType = "text/html";
            }

            StorageDto storage = await _storageProvider.AddAsync(
                uploadAt,
                userid,
                path,
                fileName,
                imageDataBytes,
                contentType
            );
            return storage.Slug;
        }
        return dataOrUrl;
    }

    private bool InvalidFileName(string fileName)
    {
        string[] fileExtensions = _fileExtensions ?? SpotLightsConstant.FileExtensions;
        return fileExtensions.Any(fileName.EndsWith);
    }

    private static string GetFileName(string fileName)
    {
        // some browsers pass uploaded file name as short file name
        // and others include the path; remove path part if needed
        const string slash = "/";
        if (fileName.Contains(slash))
        {
            fileName = fileName[fileName.LastIndexOf(slash)..];
            fileName = fileName.Replace(slash, "");
        }
        // when drag-and-drop or copy image to TinyMce editor
        // it uses "mceclip0" as file name; randomize it for multiple uploads
        if (fileName.StartsWith("mceclip0"))
        {
            Random rnd = new();
            fileName = fileName.Replace("mceclip0", rnd.Next(100000, 999999).ToString());
        }
        return fileName.SanitizePath();
    }

    private static string GetFileNameByUrl(string uri)
    {
        string title = uri.ToLower();
        title = title.Replace("%2f", "/");

        if (title.EndsWith(".axdx"))
        {
            title = title.Replace(".axdx", "");
        }
        if (title.Contains("image.axd?picture="))
        {
            title = title[(title.IndexOf("image.axd?picture=") + 18)..];
        }
        if (title.Contains("file.axd?file="))
        {
            title = title[(title.IndexOf("file.axd?file=") + 14)..];
        }
        if (title.Contains("encrypted-tbn") || title.Contains("base64,"))
        {
            Random rnd = new();
            title = string.Format("{0}.png", rnd.Next(1000, 9999));
        }
        if (title.Contains('/'))
        {
            title = title[title.LastIndexOf("/")..];
        }
        title = title.Replace(" ", "-");
        return title.Replace("/", "").SanitizeFileName();
    }

    #region no use code

    //public async Task<string> UploadBase64Image(string baseImg, string root, string path = "")
    //{
    //  path = path.Replace("/", _slash);
    //  var fileName = "";

    //  VerifyPath(_pathLocalRoot, path);
    //  string imgSrc = GetImgSrcValue(baseImg);

    //  var rnd = new Random();
    //  if (imgSrc.StartsWith("data:image/png;base64,"))
    //  {
    //    fileName = string.Format("{0}.png", rnd.Next(1000, 9999));
    //    imgSrc = imgSrc.Replace("data:image/png;base64,", "");
    //  }
    //  if (imgSrc.StartsWith("data:image/jpeg;base64,"))
    //  {
    //    fileName = string.Format("{0}.jpeg", rnd.Next(1000, 9999));
    //    imgSrc = imgSrc.Replace("data:image/jpeg;base64,", "");
    //  }
    //  if (imgSrc.StartsWith("data:image/gif;base64,"))
    //  {
    //    fileName = string.Format("{0}.gif", rnd.Next(1000, 9999));
    //    imgSrc = imgSrc.Replace("data:image/gif;base64,", "");
    //  }

    //  var filePath = string.IsNullOrEmpty(path) ?
    //       Path.Combine(_pathLocalRoot, fileName) :
    //       Path.Combine(_pathLocalRoot, path + _slash + fileName);

    //  await File.WriteAllBytesAsync(filePath, Convert.FromBase64String(imgSrc));

    //  return $"![{fileName}]({root}{PathToUrl(filePath)})";
    //}

    //static void VerifyPath(string basePath, string path)
    //{
    //  path = path.SanitizePath();

    //  if (!string.IsNullOrEmpty(path))
    //  {
    //    var dir = Path.Combine(basePath, path);

    //    if (!Directory.Exists(dir))
    //    {
    //      Directory.CreateDirectory(dir);
    //    }
    //  }
    //}
    //string PathToUrl(string path)
    //{
    //  string url = path.ReplaceIgnoreCase(_pathLocalRoot, "").Replace(_slash, "/");
    //  return $"data/{url}";
    //}
    //static string GetImgSrcValue(string imgTag)
    //{
    //  if (!(imgTag.Contains("data:image") && imgTag.Contains("src=")))
    //    return imgTag;

    //  int start = imgTag.IndexOf("src=");
    //  int srcStart = imgTag.IndexOf("\"", start) + 1;

    //  if (srcStart < 2)
    //    return imgTag;

    //  int srcEnd = imgTag.IndexOf("\"", srcStart);

    //  if (srcEnd < 1 || srcEnd <= srcStart)
    //    return imgTag;

    //  return imgTag.Substring(srcStart, srcEnd - srcStart);
    //}
    #endregion

    #region theme no use code

    //public async Task<IList<string>> GetThemesAsync()
    //{
    //  var themes = new List<string>();
    //  var themesDirectory = Path.Combine(ContentRoot, $"Views{_slash}Themes");
    //  foreach (string dir in Directory.GetDirectories(themesDirectory))
    //  {
    //    themes.Add(Path.GetFileName(dir));
    //  }
    //  return await Task.FromResult(themes);
    //}

    //public async Task<ThemeSettings?> GetThemeSettingsAsync(string theme)
    //{
    //  var settings = new ThemeSettings();
    //  var fileName = Path.Combine(ContentRoot, $"wwwroot{_slash}themes{_slash}{theme.ToLower()}{_slash}settings.json");
    //  if (File.Exists(fileName))
    //  {
    //    try
    //    {
    //      string jsonString = File.ReadAllText(fileName);
    //      settings = JsonSerializer.Deserialize<ThemeSettings>(jsonString);
    //    }
    //    catch (Exception ex)
    //    {
    //      _logger.LogError("Error reading theme settings: {Message}", ex.Message);
    //      return null;
    //    }
    //  }

    //  return await Task.FromResult(settings);
    //}

    //public async Task<bool> SaveThemeSettingsAsync(string theme, ThemeSettings settings)
    //{
    //  var fileName = Path.Combine(ContentRoot, $"wwwroot{_slash}themes{_slash}{theme.ToLower()}{_slash}settings.json");
    //  try
    //  {
    //    if (File.Exists(fileName))
    //      File.Delete(fileName);

    //    var options = new JsonSerializerOptions { WriteIndented = true, PropertyNameCaseInsensitive = true };

    //    string jsonString = JsonSerializer.Serialize(settings, options);

    //    using FileStream createStream = File.Create(fileName);
    //    await JsonSerializer.SerializeAsync(createStream, settings, options);
    //  }
    //  catch (Exception ex)
    //  {
    //    _logger.LogError("Error writing theme settings: {Message}", ex.Message);
    //    return false;
    //  }
    //  return true;
    //}
    #endregion
}
