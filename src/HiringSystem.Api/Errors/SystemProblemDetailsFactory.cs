using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;

namespace HiringSystem.Api.Errors;

public class SystemProblemDetailsFactory : ProblemDetailsFactory
{
   private readonly ApiBehaviorOptions _apiBehaviorOptions;

   public SystemProblemDetailsFactory(IOptions<ApiBehaviorOptions> apiBehaviorOptions)
   {
       _apiBehaviorOptions = apiBehaviorOptions.Value;
   }

   public override ProblemDetails CreateProblemDetails(HttpContext httpContext, int? statusCode = null, string? title = null,
        string? type = null, string? detail = null, string? instance = null)
    {
        statusCode ??= 500;
        
        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Type = type,
            Detail = detail,
            Instance = instance
        };
        
        ApplyProblemDetailsDefaults(httpContext, problemDetails, statusCode.Value);
        
        return problemDetails;
    }
   
    private void ApplyProblemDetailsDefaults(HttpContext httpContext, ProblemDetails problemDetails, int statusCode)
    {
        if (_apiBehaviorOptions.ClientErrorMapping.TryGetValue(statusCode, out var clientErrorData))
        {
            problemDetails.Title ??= clientErrorData.Title;
            problemDetails.Type ??= clientErrorData.Link;
        }
        
        var traceId = Activity.Current?.Id ?? httpContext.TraceIdentifier;
        problemDetails.Extensions["traceId"] = traceId;
    }

    public override ValidationProblemDetails CreateValidationProblemDetails(HttpContext httpContext,
        ModelStateDictionary modelStateDictionary, int? statusCode = null, string? title = null, string? type = null,
        string? detail = null, string? instance = null)
    {
        statusCode ??= 400;
        
        var problemDetails = new ValidationProblemDetails(modelStateDictionary)
        {
            Status = statusCode,
            Title = title,
            Type = type,
            Detail = detail,
            Instance = instance
        };
        
        ApplyProblemDetailsDefaults(httpContext, problemDetails, statusCode.Value);
        
        return problemDetails;
    }
}