using System.Security.Cryptography;

namespace IdentityServer.Application.Utils;

public static class PasswordHelper
{
    public static (string Hash, string Salt) HashPassword(string password)
    {
        var salt = GenerateSalt(16);
        var hash = HashWithSalt(password, salt);

        return (hash, salt);
    }

    public static bool PasswordsMatch(string inputPassword, string storedHash, string storedSalt)
    {
        var hashedInput = HashWithSalt(inputPassword, storedSalt);
        return hashedInput == storedHash;
    }

    private static string GenerateSalt(int length)
    {
        var saltBytes = RandomNumberGenerator.GetBytes(length);
        return Convert.ToBase64String(saltBytes);
    }

    public static string HashWithSalt(string password, string salt)
    {
        using var pbkdf2 = new Rfc2898DeriveBytes(password, Convert.FromBase64String(salt), 10000, HashAlgorithmName.SHA256);
        return Convert.ToBase64String(pbkdf2.GetBytes(32));
    }
}