using IdentityServer.Application.DTOs;
using IdentityServer.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.API.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/auth")]
public class TenantController(ITenantService tenantService) : BaseController
{
    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateTenantDto createTenantDto)
    {
        var success = await tenantService.CreateAsync(createTenantDto);
        if (!success) return Error("Registration failed.");
        return Success("User registered successfully.");
    }
}
