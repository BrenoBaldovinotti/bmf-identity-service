using IdentityServer.Application.DTOs;
using IdentityServer.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.API.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/auth")]
public class AuthController(IAuthService authService) : BaseController
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto loginDto)
    {
        var token = await authService.LoginAsync(loginDto);
        if (token == null) return Unauthorized("Invalid credentials.");
        return Success(new { Token = token });
    }
}
