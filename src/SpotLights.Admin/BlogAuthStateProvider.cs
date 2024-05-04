using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;
using SpotLights.Shared.Entities.Identity;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace SpotLights.Admin;

public class BlogAuthStateProvider : AuthenticationStateProvider
{
    private readonly ILogger _logger;
    protected readonly HttpClient _httpClient;
    protected AuthenticationState? _state;

    public BlogAuthStateProvider(ILogger<BlogAuthStateProvider> logger, HttpClient httpClient)
    {
        _logger = logger;
        _httpClient = httpClient;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        if (_state == null)
        {
            HttpResponseMessage response = await _httpClient.GetAsync("/api/token/userinfo");
            IdentityClaims? claims = null;
            if (response.IsSuccessStatusCode)
            {
                System.IO.Stream stream = await response.Content.ReadAsStreamAsync();
                if (stream.Length > 0)
                {
                    claims = JsonSerializer.Deserialize<IdentityClaims>(
                        stream,
                        SpotLightsSharedConstant.DefaultJsonSerializerOptions
                    )!;
                    _logger.LogInformation("claims success userName:{UserName}", claims.UserName);
                }
            }
            else
            {
                _logger.LogError("claims http error StatusCode:{StatusCode}", response.StatusCode);
            }
            System.Security.Claims.ClaimsPrincipal principal = IdentityClaims.Generate(claims);
            _state = new AuthenticationState(principal);
        }
        return _state;
    }
}
