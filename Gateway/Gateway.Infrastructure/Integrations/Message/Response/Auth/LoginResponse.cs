using System.Text.Json.Serialization;

namespace Gateway.Infrastructure.Integrations.Message.Response.Auth;

public class LoginResponse
{
    [JsonPropertyName("access_token")]
    public string? Token { get; set; }

    [JsonPropertyName("expiration")]
    public string? Expiration { get; set; }
}
