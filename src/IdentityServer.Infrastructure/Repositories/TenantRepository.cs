using IdentityServer.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using IdentityServer.Domain.Entities;
using IdentityServer.Domain.Repository;

namespace IdentityServer.Infrastructure.Repositories;

public class TenantRepository(IdentityDbContext context) : ITenantRepository
{
    public async Task AddAsync(Tenant tenant)
    {
        await context.Tenants.AddAsync(tenant);
        await context.SaveChangesAsync();
    }

    public async Task<Tenant?> GetByApiKeyHash(string apiKeyHash)
    {
        return await context.Tenants.FirstOrDefaultAsync(t => t.ApiKeyHash == apiKeyHash);
    }
}
