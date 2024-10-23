using System.Security.Cryptography;

namespace IdentityServer.Application.Utils;

public static class PasswordHasher
{
    public static (string Hash, string Salt) HashPassword(string password)
    {
        var salt = GenerateSalt(16);
        var hash = HashWithSalt(password, salt);

        return (hash, salt);
    }

    public static bool VerifyPassword(string password, string storedHash, string storedSalt)
    {
        var hash = HashWithSalt(password, storedSalt);
        return hash == storedHash;
    }

    private static string GenerateSalt(int length)
    {
        var saltBytes = RandomNumberGenerator.GetBytes(length);
        return Convert.ToBase64String(saltBytes);
    }

    private static string HashWithSalt(string password, string salt)
    {
        using var pbkdf2 = new Rfc2898DeriveBytes(password, Convert.FromBase64String(salt), 10000, HashAlgorithmName.SHA256);
        return Convert.ToBase64String(pbkdf2.GetBytes(32));
    }
}