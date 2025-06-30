namespace Donor.API.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception");

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = ex is InvalidOperationException ? StatusCodes.Status400BadRequest : StatusCodes.Status500InternalServerError;

            var result = System.Text.Json.JsonSerializer.Serialize(new
            {
                mensagem = ex is InvalidOperationException ? ex.Message : "Ocorreu um erro inesperado"
            });

            await context.Response.WriteAsync(result);
        }
    }
}