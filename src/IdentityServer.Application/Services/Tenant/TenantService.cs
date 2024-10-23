using IdentityServer.Application.DTOs;
using IdentityServer.Application.Utils;
using IdentityServer.Domain.Entities;
using IdentityServer.Domain.Repository;

namespace IdentityServer.Application.Services;

public class TenantService(ITenantRepository tenantRepository) : ITenantService
{
    public async Task<bool> CreateAsync(CreateTenantDto createTenantDto)
    {
        var apiKey = ApiKeyGenerator.GenerateApiKey();
        var hashedApiKey = ApiKeyGenerator.HashApiKey(apiKey);

        var tenant = new Tenant
        {
            Name = createTenantDto.Name,
            ApiKeyHash = hashedApiKey,
            CreatedAt = DateTime.UtcNow,
        };

        await tenantRepository.AddAsync(tenant);

        return true;
    }
}
    