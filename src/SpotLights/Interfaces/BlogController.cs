using SpotLights.Data;
using SpotLights.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Mapster;
using SpotLights.Infrastructure.Repositories.Blogs;

namespace SpotLights.Interfaces;

[ApiController]
[Authorize]
[Route("api/blog")]
public class BlogController : ControllerBase
{
    private readonly BlogRepository _blogManager;

    public BlogController(BlogRepository blogManager)
    {
        _blogManager = blogManager;
    }

    [HttpGet]
    public async Task<BlogEitorDto> GetAsync()
    {
        var data = await _blogManager.GetAsync();
        var dataDto = data.Adapt<BlogEitorDto>();
        return dataDto;
    }

    [HttpPut]
    public async Task PutAsync([FromBody] BlogEitorDto blog)
    {
        var data = await _blogManager.GetAsync();
        data.Title = blog.Title;
        data.Description = blog.Description;
        data.HeaderScript = blog.HeaderScript;
        data.FooterScript = blog.FooterScript;
        data.IncludeFeatured = blog.IncludeFeatured;
        data.ItemsPerPage = blog.ItemsPerPage;
        await _blogManager.SetAsync(data);
    }

    [HttpGet("about")]
    public AboutDto GetAboutAsync()
    {
        var result = new AboutDto
        {
            Version = typeof(BlogController)
                ?.GetTypeInfo()
                ?.Assembly?.GetCustomAttribute<AssemblyInformationalVersionAttribute>()
                ?.InformationalVersion,
            DatabaseProvider = "", //dbContext.Database.ProviderName,
            OperatingSystem = RuntimeInformation.OSDescription
        };
        return result;
    }
}
