namespace Donation.API.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
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
            context.Response.StatusCode = ex is InvalidOperationException ? StatusCodes.Status400BadRequest : StatusCodes.Status500InternalServerError;

            object errorResponse = new
            {
                Message = ex is InvalidOperationException ? ex.Message : "Ocorreu um erro inesperado"
            };

            await context.Response.WriteAsJsonAsync(errorResponse);
        }
    }
}