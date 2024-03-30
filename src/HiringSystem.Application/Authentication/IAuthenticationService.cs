namespace HiringSystem.Application.Authentication;

public interface IAuthenticationService
{
    Task<AuthenticationResponse> Register(string name, string email, string password);
    Task<AuthenticationResponse> Login(string email, string password);
}