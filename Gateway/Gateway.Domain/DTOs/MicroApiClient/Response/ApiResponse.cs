using System.Net;

namespace Gateway.Domain.DTOs.MicroApiClient.Response;

public class ApiResponse
{
    public HttpStatusCode StatusCode { get; set; }

    public string Content { get; set; }

    public ApiResponse(HttpStatusCode statusCode, string response)
    {
        StatusCode = statusCode;
        Content = response;
    }
}