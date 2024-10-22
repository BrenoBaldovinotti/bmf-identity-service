using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Domain.Entities;

public class User : IdentityUser<Guid>
{
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
