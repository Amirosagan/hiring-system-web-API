using System.Net;
using Microsoft.AspNetCore.Mvc;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace HiringSystem.Api.Middleware;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next)
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
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var result = JsonSerializer.Serialize(new ProblemDetails
        {
            Title = "An error occurred",
            Status = 500
        });
        
        context.Response.ContentType = "application/problem+json";
        
        context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
        
        await context.Response.WriteAsync(result);
    }
}