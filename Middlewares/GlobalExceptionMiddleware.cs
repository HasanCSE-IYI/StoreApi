using System.Net;
using System.Text.Json;

namespace StoreApi.Middlewares;

public class GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger, IHostEnvironment env)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An unhandled exception occurred: {Message}", ex.Message);
            await HandleExceptionAsync(context, ex, env.IsDevelopment());
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception, bool isDevelopment)
    {
        if (context.Response.HasStarted)
        {
            logger.LogWarning("The response has already started, cannot write error response.");
            return Task.CompletedTask;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var response = new
        {
            status = context.Response.StatusCode,
            message = "An internal server error occurred. Please try again later.",
            detailed = isDevelopment ? exception.Message : null // শুধুমাত্র ডেভেলপমেন্টে ডিটেইল দেখাবে
        };

        var json = JsonSerializer.Serialize(response);
        return context.Response.WriteAsync(json);
    }
}