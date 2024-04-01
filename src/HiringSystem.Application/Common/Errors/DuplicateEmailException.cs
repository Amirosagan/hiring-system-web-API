using System.Net;

namespace HiringSystem.Application.Common.Errors;

public class DuplicateEmailException : Exception, IServiceException
{
    public string ErrorMessage => "Email already exists";
    public HttpStatusCode ErrorCode => HttpStatusCode.BadRequest;
}