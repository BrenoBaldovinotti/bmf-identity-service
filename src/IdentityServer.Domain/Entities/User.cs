using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Domain.Entities;

public class User : IdentityUser<Guid>
{
    public string PasswordSalt { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public User() : base() { }

    public User(
        string username,
        string email, 
        string password, 
        string passwordSalt,
        string? phoneNumber) : base(username)
    {
        NormalizedUserName = username;
        Email = email;
        NormalizedEmail = email.ToUpper();
        EmailConfirmed = false;
        PhoneNumber = phoneNumber;
        PhoneNumberConfirmed = false;
        TwoFactorEnabled = false;
        LockoutEnabled = true;
        AccessFailedCount = 0;
        CreatedAt = DateTime.UtcNow;
        PasswordHash = password;
        PasswordSalt = passwordSalt;
    }
}
