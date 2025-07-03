using Gateway.Domain.Exceptions;

namespace Gateway.API.Middleware;

public class ErrorHandlingGatewayMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlingGatewayMiddleware(RequestDelegate next)
    {
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

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        if (exception is HttpRequestFailGatewayException gatewayException)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = gatewayException!.StatusCode;
           
            object errorResponse = new
            {
                message = gatewayException.Message
            };

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

            return context.Response.WriteAsJsonAsync(errorResponse);
        }

        context.Response.StatusCode = 500;
        return context.Response.WriteAsJsonAsync(new
        {
            message = "Ocorreu um erro no sistema, tente novamente mais tarde"
        });
    }
}