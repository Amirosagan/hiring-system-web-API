using ErrorOr;
using HiringSystem.Application.Authentication.Common;
using HiringSystem.Application.Common.Interfaces.Authentication;
using HiringSystem.Application.Common.Interfaces.Persistence;
using HiringSystem.Application.Common.Interfaces.Services;
using HiringSystem.Domain.Common.Errors;
using MediatR;

namespace HiringSystem.Application.Authentication.Queries.Login;

public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResponse>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public Task<ErrorOr<AuthenticationResponse>> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var user = _userRepository.GetUserByEmail(request.Email);
        
        if (user == null)
        {
            return Task.FromResult<ErrorOr<AuthenticationResponse>>(Errors.User.NotFound(request.Email));
        }
        
        if (!_passwordHasher.VerifyPassword(request.Password, user.Password))
        {
            return Task.FromResult<ErrorOr<AuthenticationResponse>>(Errors.User.InvalidPassword());
        }

        var token = _jwtTokenGenerator.GenerateToken(user.Id, user.Name, request.Email);

        return Task.FromResult<ErrorOr<AuthenticationResponse>>(new AuthenticationResponse(user.Id, token));
    }
}