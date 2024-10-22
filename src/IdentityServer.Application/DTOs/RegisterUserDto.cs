using System.Text.Json.Serialization;

namespace IdentityServer.Application.DTOs;

public class RegisterUserDto
{
    [JsonPropertyName("username")]
    public string Username { get; set; } = string.Empty;
    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;
    [JsonPropertyName("password")]
    public string Password { get; set; } = string.Empty;
    [JsonPropertyName("application-key")]
    public string ApplicationKey { get; set; } = string.Empty;
    [JsonPropertyName("phone-number")]
    public string? PhoneNumber { get; set; }
}
