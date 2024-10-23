using IdentityServer.Application.DTOs;
using IdentityServer.Application.Utils;
using IdentityServer.Infrastructure.Repositories.Tenant;

namespace IdentityServer.Application.Services.Tenant;

public class TenantService(ITenantRepository tenantRepository) : ITenantService
{
    public async Task<bool> CreateAsync(CreateTenantDto createTenantDto)
    {
        var apiKey = ApiKeyGenerator.GenerateApiKey();
        var hashedApiKey = ApiKeyGenerator.HashApiKey(apiKey);

        var tenant = new Domain.Entities.Tenant
        {
            Name = createTenantDto.Name,
            ApiKeyHash = hashedApiKey,
            CreatedAt = DateTime.UtcNow,
        };

        await tenantRepository.AddTenantAsync(tenant);

        return true;
    }
}
    