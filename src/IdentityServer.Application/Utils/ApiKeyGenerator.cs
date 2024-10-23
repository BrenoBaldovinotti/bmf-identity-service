using System.Security.Cryptography;
using System.Text;

namespace IdentityServer.Application.Utils;

public static class ApiKeyGenerator
{
    public static string GenerateApiKey()
    {
        var keyBytes = RandomNumberGenerator.GetBytes(32);
        return Convert.ToBase64String(keyBytes);
    }

    public static string HashApiKey(string apiKey)
    {
        var hashBytes = SHA256.HashData(Encoding.UTF8.GetBytes(apiKey));
        return Convert.ToBase64String(hashBytes);
    }
}
