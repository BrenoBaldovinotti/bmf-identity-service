using IdentityServer.Application.DTOs;
using IdentityServer.Application.Services;
using IdentityServer.Application.Services.User;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Presentation.Controllers;

[ApiController]
[Route("api/v1/auth")]
public class AuthController(IUserService userService) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
    {
        var token = await userService.LoginAsync(request);
        return Ok(new { Token = token });
    }
}
