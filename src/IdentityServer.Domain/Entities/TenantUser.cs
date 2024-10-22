namespace IdentityServer.Domain.Entities;

public class TenantUser
{
    public Guid UserId { get; set; }
    public Guid TenantId { get; set; }
}
