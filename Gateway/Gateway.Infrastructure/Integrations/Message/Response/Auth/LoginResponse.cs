using System.Text.Json.Serialization;

namespace Gateway.Infrastructure.Integrations.Message.Response.Auth;

public class LoginResponse
{
    public string? Token { get; set; }

    public string? Expiration { get; set; }
}
