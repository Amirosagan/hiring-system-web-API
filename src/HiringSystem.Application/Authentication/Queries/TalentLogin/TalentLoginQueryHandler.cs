using ErrorOr;

using HiringSystem.Application.Authentication.Common;
using HiringSystem.Application.Common.Interfaces.Authentication;
using HiringSystem.Application.Common.Interfaces.Persistence;
using HiringSystem.Application.Common.Interfaces.Services;
using HiringSystem.Domain.Common.Errors;

using MediatR;

namespace HiringSystem.Application.Authentication.Queries.TalentLogin;

public class TalentLoginQueryHandler : IRequestHandler<TalentLoginQuery, ErrorOr<AuthenticationResponse>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly ITalentRepository _talentRepository;
    private readonly IPasswordHasher _passwordHasher;


    public TalentLoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, ITalentRepository talentRepository, IPasswordHasher passwordHasher)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _talentRepository = talentRepository;
        _passwordHasher = passwordHasher;
    }

    public Task<ErrorOr<AuthenticationResponse>> Handle(TalentLoginQuery request, CancellationToken cancellationToken)
    {
        var user = _talentRepository.GetTalentByEmail(request.Email);
        
        if (ReferenceEquals(user, null))
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