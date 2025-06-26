namespace Gateway.Infrastructure.Integrations.Message.Request.Auth;

public class LoginRequest
{
    public string? Identifier { get; set; }
    public string? Password { get; set; }
}
