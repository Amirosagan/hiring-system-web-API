using ErrorOr;
using HiringSystem.Application.Authentication.Common;
using HiringSystem.Application.Common.Interfaces.Authentication;
using HiringSystem.Application.Common.Interfaces.Persistence;
using HiringSystem.Domain.Common.Errors;
using HiringSystem.Domain.Entities;
using MediatR;

namespace HiringSystem.Application.Authentication.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResponse>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;


    public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public Task<ErrorOr<AuthenticationResponse>> Handle(RegisterCommand register, CancellationToken cancellationToken)
    {
        if (_userRepository.GetUserByEmail(register.Email) != null)
        {
            return Task.FromResult<ErrorOr<AuthenticationResponse>>(Errors.User.DuplicateEmail(register.Email));
        }

        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = register.Name,
            Email = register.Email,
            Password = register.Password
        };

        _userRepository.AddUser(user);

        var token = _jwtTokenGenerator.GenerateToken(user.Id, register.Name, register.Email);

        return Task.FromResult<ErrorOr<AuthenticationResponse>>(new AuthenticationResponse(user.Id, token));

    }
}