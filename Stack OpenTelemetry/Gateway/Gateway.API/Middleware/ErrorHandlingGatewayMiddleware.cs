using Gateway.Domain.Exceptions;

namespace Gateway.API.Middleware;

public class ErrorHandlingGatewayMiddleware
{
    private readonly ILogger<ErrorHandlingGatewayMiddleware> _logger;
    private readonly RequestDelegate _next;

    public ErrorHandlingGatewayMiddleware(ILogger<ErrorHandlingGatewayMiddleware> logger, RequestDelegate next)
    {
        _logger = logger;
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);

            throw;
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        if (exception is HttpRequestFailGatewayException gatewayException)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = gatewayException!.StatusCode;

            object errorResponse = new
            {
                message = gatewayException.Message
            };

            _logger.LogError(gatewayException,
               "Exception: {Ex} | Path: {Path} | TraceId: {TraceId} | StatusCode: {StatusCode}",
               gatewayException.Message,
               context.Request.Path,
               context.TraceIdentifier,
               context.Response.StatusCode);

            return context.Response.WriteAsJsonAsync(errorResponse);
        }

        if (exception is InvalidOperationException invalidOperationException)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            object errorResponse = new
            {
                message = invalidOperationException.Message
            };

            _logger.LogError(invalidOperationException,
               "Exception: {Ex} | Path: {Path} | TraceId: {TraceId} | StatusCode: {StatusCode}",
               invalidOperationException.Message,
               context.Request.Path,
               context.TraceIdentifier,
               context.Response.StatusCode);

            return context.Response.WriteAsJsonAsync(errorResponse);
        }

        context.Response.StatusCode = 500;

        _logger.LogError(exception,
            "Exception: {Ex} | Path: {Path} | TraceId: {TraceId} | StatusCode: {StatusCode}",
            exception.Message,
            context.Request.Path,
            context.TraceIdentifier,
            context.Response.StatusCode);

        return context.Response.WriteAsJsonAsync(new
        {
            message = "Ocorreu um erro no sistema, tente novamente mais tarde"
        });
    }
}