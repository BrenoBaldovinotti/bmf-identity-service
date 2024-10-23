namespace IdentityServer.Infrastructure.Repositories.Tenant;

public interface ITenantRepository
{
    Task AddTenantAsync(Domain.Entities.Tenant tenant);
    Task<Domain.Entities.Tenant?> GetByApiKeyHash(string apiKeyHash);
}
