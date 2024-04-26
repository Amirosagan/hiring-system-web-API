using ErrorOr;
using HiringSystem.Application.Authentication.Common;
using HiringSystem.Application.Common.Interfaces.Authentication;
using HiringSystem.Application.Common.Interfaces.Persistence;
using HiringSystem.Application.Common.Interfaces.Services;
using HiringSystem.Domain.Common.Errors;
using HiringSystem.Domain.Entities;
using MediatR;

namespace HiringSystem.Application.Authentication.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResponse>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;


    public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public Task<ErrorOr<AuthenticationResponse>> Handle(RegisterCommand register, CancellationToken cancellationToken)
    {
        if (_userRepository.GetUserByEmail(register.Email) != null)
        {
            return Task.FromResult<ErrorOr<AuthenticationResponse>>(Errors.User.DuplicateEmail(register.Email));
        }
        
        string hashedPassword = _passwordHasher.HashPassword(register.Password);

        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = register.Name,
            Email = register.Email,
            Password = hashedPassword
        };

        _userRepository.AddUser(user);

        var token = _jwtTokenGenerator.GenerateToken(user.Id, register.Name, register.Email);

        return Task.FromResult<ErrorOr<AuthenticationResponse>>(new AuthenticationResponse(user.Id, token));

    }
}