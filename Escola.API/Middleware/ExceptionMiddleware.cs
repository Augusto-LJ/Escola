using Escola.API.Errors;
using Escola.Application.Exceptions;
using System.Text.Json;

namespace Escola.API.Middleware;

public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<ExceptionMiddleware> _logger = logger;
    private readonly IHostEnvironment _env = env;

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);

            int statusCode = ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                BadRequestException => StatusCodes.Status400BadRequest,
                UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
                _ => StatusCodes.Status500InternalServerError
            };

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = statusCode;

            var response = _env.IsDevelopment()
                ? new ApiException(statusCode.ToString(), ex.Message, ex.StackTrace?.ToString())
                : new ApiException(statusCode.ToString(), ex.Message, "Internal Server Error");

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var json = JsonSerializer.Serialize(response, options);

            await httpContext.Response.WriteAsJsonAsync(response);
        }
    }
}
