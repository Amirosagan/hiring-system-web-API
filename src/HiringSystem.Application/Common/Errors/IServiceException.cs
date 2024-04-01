using System.Net;

namespace HiringSystem.Application.Common.Errors;

public interface IServiceException
{
    public string ErrorMessage { get; } 
    public HttpStatusCode ErrorCode { get; }
}