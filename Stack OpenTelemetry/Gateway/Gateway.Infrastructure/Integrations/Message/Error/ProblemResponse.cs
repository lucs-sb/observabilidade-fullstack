using System.Text.Json.Serialization;

namespace Gateway.Infrastructure.Integrations.Message.Error;

public class ProblemResponse
{
    [JsonPropertyName("message")]
    public string? Message { get; set; }
}
