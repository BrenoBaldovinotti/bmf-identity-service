using IdentityServer.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Infrastructure.Repositories.Tenant;

public class TenantRepository(IdentityDbContext context) : ITenantRepository
{
    public async Task AddTenantAsync(Domain.Entities.Tenant tenant)
    {
        await context.Tenants.AddAsync(tenant);
        await context.SaveChangesAsync();
    }

    public async Task<Domain.Entities.Tenant?> GetByApiKeyHash(string apiKeyHash)
    {
        return await context.Tenants.FirstOrDefaultAsync(t => t.ApiKeyHash == apiKeyHash);
    }
}
    