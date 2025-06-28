namespace Gateway.Domain.Exceptions;

public class HttpRequestFailGatewayException : Exception
{
    public readonly int StatusCode;

    public HttpRequestFailGatewayException(int statusCode, string message)
        : base(message)
    {
        StatusCode = statusCode;
    }
}