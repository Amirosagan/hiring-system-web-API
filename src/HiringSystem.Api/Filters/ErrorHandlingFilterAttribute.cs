using System.Net;
using HiringSystem.Application.Common.Errors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace HiringSystem.Api.Filters;

public class ErrorHandlingFilterAttribute : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        var exception = context.Exception;
        
        var (statusCode, message) = exception switch
        {
            DuplicateEmailException serviceException => ((int) serviceException.ErrorCode, serviceException.ErrorMessage),
            _ => ((int) HttpStatusCode.InternalServerError, "An error occurred")
        };
        
        var problemDetails = new ProblemDetails
        {
            Title = message, 
            Status = statusCode
        };
        
        context.Result = new ObjectResult(problemDetails)
        {
            StatusCode = problemDetails.Status
        };
        
        context.ExceptionHandled = true;
    }
}