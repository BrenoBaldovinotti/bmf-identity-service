using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Domain.Entities;

public class User : IdentityUser<Guid>
{
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public User(string name, string email, string? phoneNumber) : base(name)
    {
        NormalizedUserName = name.ToUpper();
        Email = email;
        NormalizedEmail = email.ToUpper();
        EmailConfirmed = false;
        PhoneNumber = phoneNumber;
        PhoneNumberConfirmed = false;
        TwoFactorEnabled = false;
        LockoutEnabled = true;
        AccessFailedCount = 0;
    }
}
