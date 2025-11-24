namespace Auth.Domain.Settings;

public class AuthSettings
{
    public string SecretKey { get; set; } = default!;
    public int ExpirationMinutes { get; set; }
}