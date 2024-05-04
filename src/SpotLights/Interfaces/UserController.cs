using SpotLights.Shared.Extensions;
using SpotLights.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpotLights.Infrastructure.Identity;
using SpotLights.Shared.Entities.Identity;
using SpotLights.Domain.Model.Identity;

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
        bool isAdmin = User.IsAdmin();
        return await _userProvider.GetAsync(isAdmin);
    }

    [HttpGet("{id:int}")]
    public async Task<UserInfoDto?> GetAsync([FromRoute] int id)
    {
        bool isAdmin = User.IsAdmin();
        return await _userProvider.GetAsync(id, isAdmin);
    }

    [HttpPut("{id:int?}")]
    public async Task<IActionResult> EditorAsync(
        [FromRoute] int? id,
        [FromBody] UserEditorDto input,
        [FromServices] UserManager userManager
    )
    {
        bool isAdmin = User.IsAdmin();

        if (!id.HasValue)
        {
            if (!isAdmin && input.Type == UserType.Administrator)
            {
                return StatusCode(
                    403,
                    new { error = "User does not have permission to add user." }
                );
            }

            UserInfo user =
                new(input.UserName)
                {
                    NickName = input.NickName,
                    Email = input.Email,
                    Avatar = input.Avatar,
                    Bio = input.Bio,
                    Type = input.Type,
                };
            Microsoft.AspNetCore.Identity.IdentityResult result = await userManager.CreateAsync(
                user,
                input.Password!
            );
            if (!result.Succeeded)
            {
                Microsoft.AspNetCore.Identity.IdentityError error = result.Errors.First();
                return Problem(detail: error.Description, title: error.Code);
            }
        }
        else
        {
            UserInfo user = await _userProvider.FindAsync(id.Value);
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

            Microsoft.AspNetCore.Identity.IdentityResult result = await userManager.UpdateAsync(
                user
            );
            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(input.Password))
                {
                    string token = await userManager.GeneratePasswordResetTokenAsync(user);
                    result = await userManager.ResetPasswordAsync(user, token, input.Password);
                    if (result.Succeeded)
                        return Ok();
                }
                return Ok();
            }
            Microsoft.AspNetCore.Identity.IdentityError error = result.Errors.First();
            return Problem(detail: error.Description, title: error.Code);
        }

        return Ok();
    }
}
