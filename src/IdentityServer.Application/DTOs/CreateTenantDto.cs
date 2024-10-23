using System.Text.Json.Serialization;

namespace IdentityServer.Application.DTOs;

public class CreateTenantDto
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
}
