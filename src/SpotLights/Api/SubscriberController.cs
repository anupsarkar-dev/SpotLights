using SpotLights.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using SpotLights.Domain.Model.Newsletters;
using SpotLights.Core.Interfaces;

namespace SpotLights.Interfaces;

[Route("api/subscriber")]
[ApiController]
public class SubscriberController : ControllerBase
{
    private readonly ISubscriberService _subscriberProvider;

    public SubscriberController(ISubscriberService subscriberProvider)
    {
        _subscriberProvider = subscriberProvider;
    }

    [HttpGet("items")]
    [Authorize]
    public async Task<IEnumerable<SubscriberDto>> GetItemsAsync()
    {
        return await _subscriberProvider.GetItemsAsync();
    }

    [HttpDelete("{id:int}")]
    [Authorize]
    public async Task DeleteAsync([FromRoute] int id)
    {
        await _subscriberProvider.DeleteAsync<Subscriber>(id);
        await _subscriberProvider.SaveChangesAsync();
    }

    [HttpPost("apply")]
    public async Task<IActionResult> ApplyAsync([FromBody] SubscriberApplyDto input)
    {
        int res = await _subscriberProvider.ApplyAsync(input);

        if (res == 1)
        {
            return Ok();
        }

        return BadRequest();
    }
}
