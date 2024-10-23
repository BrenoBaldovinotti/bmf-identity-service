using IdentityServer.Application.DTOs;
using IdentityServer.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.API.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/user")]
public class UserController(IUserService userService) : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterUserDto registerDto)
    {
        var success = await userService.RegisterAsync(registerDto);
        if (!success) return Error("Registration failed.");
        return Success("User registered successfully.");
    }
}
