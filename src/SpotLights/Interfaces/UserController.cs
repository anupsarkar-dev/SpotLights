using SpotLights.Identity;
using SpotLights.Shared;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SpotLights.Interfaces;

[Route("api/user")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly UserProvider _userProvider;

    public UserController(UserProvider userProvider)
    {
        _userProvider = userProvider;
    }

    [HttpGet("items")]
    public async Task<IEnumerable<UserInfoDto>> GetItemsAsync()
    {
        var isAdmin = User.IsAdmin();
        return await _userProvider.GetAsync(isAdmin);
    }

    [HttpGet("{id:int}")]
    public async Task<UserInfoDto?> GetAsync([FromRoute] int id)
    {
        var isAdmin = User.IsAdmin();
        return await _userProvider.GetAsync(id, isAdmin);
    }

    [HttpPut("{id:int?}")]
    public async Task<IActionResult> EditorAsync(
        [FromRoute] int? id,
        [FromBody] UserEditorDto input,
        [FromServices] UserManager userManager
    )
    {
        var isAdmin = User.IsAdmin();

        if (!id.HasValue)
        {
            if (!isAdmin && input.Type == UserType.Administrator)
            {
                return StatusCode(
                    403,
                    new { error = "User does not have permission to add user." }
                );
            }

            var user = new UserInfo(input.UserName)
            {
                NickName = input.NickName,
                Email = input.Email,
                Avatar = input.Avatar,
                Bio = input.Bio,
                Type = input.Type,
            };
            var result = await userManager.CreateAsync(user, input.Password!);
            if (!result.Succeeded)
            {
                var error = result.Errors.First();
                return Problem(detail: error.Description, title: error.Code);
            }
        }
        else
        {
            var user = await _userProvider.FindAsync(id.Value);
            user.NickName = input.NickName;
            user.Avatar = input.Avatar;
            user.Bio = input.Bio;
            user.Type = input.Type;

            if (
                !isAdmin
                && (user.Type == UserType.Administrator || input.Type == UserType.Administrator)
            )
            {
                return StatusCode(
                    403,
                    new { error = "User does not have permission to update user." }
                );
            }

            var result = await userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(input.Password))
                {
                    var token = await userManager.GeneratePasswordResetTokenAsync(user);
                    result = await userManager.ResetPasswordAsync(user, token, input.Password);
                    if (result.Succeeded)
                        return Ok();
                }
                return Ok();
            }
            var error = result.Errors.First();
            return Problem(detail: error.Description, title: error.Code);
        }

        return Ok();
    }
}
