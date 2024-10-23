using IdentityServer.Application.DTOs;
using IdentityServer.Application.Services;
using IdentityServer.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.API.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/user")]
public class UserController(IUserService userService) : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterUserDto registerDto)
    {
        var tenant = HttpContext.Items["Tenant"] as Tenant;
        if (tenant == null) return Unauthorized();

        var success = await userService.RegisterAsync(registerDto, tenant);
        if (!success) return Error("Registration failed.");
        return Success("User registered successfully.");
    }
}
