using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace HiringSystem.Api.Controllers;

[ApiController]
public class ErrorApiController : ControllerBase
{
    protected IActionResult Problem(List<Error> errors)
    {
        if (errors.All(x => x.Type == ErrorType.Validation))
        {
            var validationErrors = new ModelStateDictionary();
            foreach (var error in errors)
            {
                validationErrors.AddModelError(error.Code, error.Description);
            }

            return ValidationProblem(title:"Validation Error", modelStateDictionary:validationErrors);
        }
        
        HttpContext.Items["Errors"] = errors;
        
        var firstError = errors.First();

        var statusCode = firstError.Type switch
        {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status400BadRequest
        };

        return Problem(statusCode: statusCode, detail: firstError.Description);
    }
    
}