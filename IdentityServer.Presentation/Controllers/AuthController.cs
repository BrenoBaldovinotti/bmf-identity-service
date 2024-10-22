using IdentityServer.Application.DTOs;
using IdentityServer.Application.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Presentation.Controllers;

[ApiController]
[Route("api/v1/auth")]
public class AuthController(IAuthService _authService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserDto registerDto)
    {
        var success = await _authService.RegisterAsync(registerDto);
        if (!success) return BadRequest("Registration failed.");
        return Ok("User registered successfully.");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto loginDto)
    {
        var token = await _authService.LoginAsync(loginDto);
        if (token == null) return Unauthorized("Invalid credentials.");
        return Ok(new { Token = token });
    }
}
