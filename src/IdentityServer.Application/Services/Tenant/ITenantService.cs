using IdentityServer.Application.DTOs;

namespace IdentityServer.Application.Services;

public interface ITenantService
{
    Task<bool> CreateAsync(CreateTenantDto createTenantDto);
}
