using Gateway.Domain.DTOs.MicroApiClient.Request.Base;
using System.Text.Json.Serialization;

namespace Gateway.Domain.DTOs.MicroApiClient.Request;

public class ApiPostRequest : BaseRequest
{
    public string? Content { get; set; }
    public Dictionary<string, string>? Headers { get; set; } = new Dictionary<string, string>();

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Dictionary<string, string>? QueryParams { get; set; }
}