using System.Diagnostics;
using Gateway.Domain.Exceptions;

namespace Gateway.API.Middleware;

public class ErrorHandlingGatewayMiddleware
{
    private readonly ILogger<ErrorHandlingGatewayMiddleware> _logger;
    private readonly RequestDelegate _next;

    public ErrorHandlingGatewayMiddleware(
        ILogger<ErrorHandlingGatewayMiddleware> logger,
        RequestDelegate next)
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
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        if (exception is HttpRequestFailGatewayException gatewayException)
        {
            context.Response.StatusCode = gatewayException.StatusCode;

            _logger.LogError(gatewayException,
               "Exception: {Ex} | Path: {Path} | StatusCode: {StatusCode}",
               gatewayException.Message,
               context.Request.Path,
               context.Response.StatusCode);

            await context.Response.WriteAsJsonAsync(new
            {
                message = gatewayException.Message
            });

            return;
        }

        if (exception is InvalidOperationException invalidOperationException)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            _logger.LogError(invalidOperationException,
               "Exception: {Ex} | Path: {Path} | StatusCode: {StatusCode}",
               invalidOperationException.Message,
               context.Request.Path,
               context.Response.StatusCode);

            await context.Response.WriteAsJsonAsync(new
            {
                message = invalidOperationException.Message
            });

            return;
        }

        context.Response.StatusCode = StatusCodes.Status500InternalServerError;

        _logger.LogError(exception,
            "Exception: {Ex} | Path: {Path} | StatusCode: {StatusCode}",
            exception.Message,
            context.Request.Path,
            context.Response.StatusCode);

        await context.Response.WriteAsJsonAsync(new
        {
            message = "Ocorreu um erro no sistema, tente novamente mais tarde"
        });
    }
}