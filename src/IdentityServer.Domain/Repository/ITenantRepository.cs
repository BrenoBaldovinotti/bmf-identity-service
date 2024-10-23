using IdentityServer.Domain.Entities;

namespace IdentityServer.Domain.Repository;

public interface ITenantRepository
{
    Task AddAsync(Tenant tenant);
    Task<Tenant?> GetByApiKeyHash(string apiKeyHash);
}
