namespace Gateway.Domain.Exceptions;

public class HttpRequestFailGatewayException : Exception
{
    public readonly int StatusCode;

    public string? Message { get; set; }

    public HttpRequestFailGatewayException(int statusCode, string message)
        : base(message)
    {
        StatusCode = statusCode;
        Message = message;
    }
}