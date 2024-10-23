using IdentityServer.Domain.Entities;

namespace IdentityServer.Domain.Repository;

public interface ITenantRepository
{
    Task AddTenantAsync(Tenant tenant);
    Task<Tenant?> GetByApiKeyHash(string apiKeyHash);
}
