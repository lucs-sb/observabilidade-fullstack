using Gateway.Domain.DTOs.MicroApiClient.Request.Base;
using System.Text.Json.Serialization;

namespace Gateway.Domain.DTOs.MicroApiClient.Request;

public class ApiGetRequest : BaseRequest
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Dictionary<string, string>? QueryParams { get; set; }

    public Dictionary<string, string>? Headers { get; set; } = new Dictionary<string, string>();

    public string? Content { get; set; }
}