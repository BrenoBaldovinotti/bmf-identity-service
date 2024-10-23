using IdentityServer.Application.DTOs;

namespace IdentityServer.Application.Services.Tenant;

public interface ITenantService
{
    Task<bool> CreateAsync(CreateTenantDto createTenantDto);
}
