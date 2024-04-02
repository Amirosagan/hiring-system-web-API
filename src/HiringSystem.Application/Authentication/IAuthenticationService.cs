using ErrorOr;
namespace HiringSystem.Application.Authentication;

public interface IAuthenticationService
{
    ErrorOr<Task<AuthenticationResponse>> Register(string name, string email, string password);
    ErrorOr<Task<AuthenticationResponse>> Login(string email, string password);
}