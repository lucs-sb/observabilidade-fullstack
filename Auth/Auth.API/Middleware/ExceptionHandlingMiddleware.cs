namespace Auth.API.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger, RequestDelegate next)
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
        catch (Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = ex is UnauthorizedAccessException ? StatusCodes.Status401Unauthorized : StatusCodes.Status500InternalServerError;

            object errorResponse = new
            {
                message = ex is UnauthorizedAccessException ? ex.Message : "Ocorreu um erro inesperado"
            };

            _logger.LogError(exception,
               "Exception: {Ex} | Path: {Path} | TraceId: {TraceId} | StatusCode: {StatusCode}",
               ex.Message,
               context.Request.Path,
               context.TraceIdentifier,
               context.Response.StatusCode);

            await context.Response.WriteAsJsonAsync(errorResponse);
        }
    }
}