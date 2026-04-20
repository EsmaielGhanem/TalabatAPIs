using System.Net;
using Talabat.APIs.Errors;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Talabat.APIs.middelWares;

public class ExceptionMiddleWare
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleWare> _logger;
    private readonly IHostEnvironment _env;

    public ExceptionMiddleWare(RequestDelegate next , ILogger<ExceptionMiddleWare> logger , IHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);   // move to next middleWare
        }
        catch (Exception ex)
        {
            _logger.LogError(ex , ex.Message); 
            // Log ex at database 


            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var exeptionErrorResponse =
                _env.IsDevelopment()
                    ? new ApiExceptionResponse(500, ex.Message, ex.StackTrace.ToString())
                    : new ApiExceptionResponse(500);

            var json = JsonSerializer.Serialize(exeptionErrorResponse);
            await context.Response.WriteAsync(json);
        }
    }
}