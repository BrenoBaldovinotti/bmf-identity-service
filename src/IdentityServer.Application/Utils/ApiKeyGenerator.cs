using System.Security.Cryptography;
using System.Text;

namespace IdentityServer.Application.Utils;

public static class ApiKeyGenerator
{
    public static string GenerateApiKey()
    {
        var uuid = Guid.NewGuid().ToString();
        var salt = GenerateSalt(16);
        return $"{uuid}:{salt}";
    }

    private static string GenerateSalt(int length)
    {
        var saltBytes = RandomNumberGenerator.GetBytes(length);
        return Convert.ToBase64String(saltBytes);
    }

    public static string HashApiKey(string apiKey)
    {
        var hashBytes = SHA256.HashData(Encoding.UTF8.GetBytes(apiKey));
        return Convert.ToBase64String(hashBytes);
    }
}
