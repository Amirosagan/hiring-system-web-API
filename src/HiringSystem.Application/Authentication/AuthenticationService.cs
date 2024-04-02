using HiringSystem.Application.Common.Interfaces.Authentication;
using HiringSystem.Application.Common.Interfaces.Persistence;
using HiringSystem.Domain.Common.Errors;
using ErrorOr;
using HiringSystem.Domain.Entities;

namespace HiringSystem.Application.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;
    
    public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }
    public ErrorOr<Task<AuthenticationResponse>> Register(string name, string email, string password)
    {
        if (_userRepository.GetUserByEmail(email) != null)
        {
            return Errors.User.DuplicateEmail(email);
        }
        
        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = name,
            Email = email,
            Password = password
        };
        
        _userRepository.AddUser(user);

        var token = _jwtTokenGenerator.GenerateToken(user.Id, name, email);
        
        return Task.FromResult(new AuthenticationResponse(user.Id, token));
    }

    public ErrorOr<Task<AuthenticationResponse>> Login(string email, string password)
    {
        if (_userRepository.GetUserByEmail(email) == null)
        {
            return Errors.User.NotFound(email);
        }
        
        var user = _userRepository.GetUserByEmail(email);
        
        var token = _jwtTokenGenerator.GenerateToken(user.Id, user.Name, email);
        
        return Task.FromResult(new AuthenticationResponse(user.Id, token));
    }
}