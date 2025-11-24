using Gateway.Domain.DTOs.MicroApiClient.Request.Base;

namespace Gateway.Domain.DTOs.MicroApiClient.Request;

public class ApiPutRequest : BaseRequest
{
    public string? Content { get; set; }

    public Dictionary<string, string>? Headers { get; set; } = new Dictionary<string, string>();
}
