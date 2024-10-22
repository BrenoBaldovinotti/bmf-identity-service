using IdentityServer.Application.DTOs;
using IdentityServer.Application.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.API.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/auth")]
public class AuthController(IAuthService _authService) : BaseController
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserDto registerDto)
    {
        var success = await _authService.RegisterAsync(registerDto);
        if (!success) return Error("Registration failed.");
        return Success("User registered successfully.");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto loginDto)
    {
        var token = await _authService.LoginAsync(loginDto);
        if (token == null) return Unauthorized("Invalid credentials.");
        return Success(new { Token = token });
    }
}
