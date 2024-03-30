namespace HiringSystem.Application.Authentication;

public class AuthenticationService : IAuthenticationService
{
    public Task<AuthenticationResponse> Register(string name, string email, string password)
    {
        return Task.FromResult(new AuthenticationResponse(Guid.NewGuid(), "token"));
    }

    public Task<AuthenticationResponse> Login(string email, string password)
    {
        return Task.FromResult(new AuthenticationResponse(Guid.NewGuid(), "token"));
    }
}