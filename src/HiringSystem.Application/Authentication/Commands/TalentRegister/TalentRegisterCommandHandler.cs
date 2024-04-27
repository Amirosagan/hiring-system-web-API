using ErrorOr;

using HiringSystem.Application.Authentication.Common;
using HiringSystem.Application.Common.Interfaces.Authentication;
using HiringSystem.Application.Common.Interfaces.Persistence;
using HiringSystem.Application.Common.Interfaces.Services;
using HiringSystem.Domain.Common.Errors;
using HiringSystem.Domain.Talent;

using MediatR;

namespace HiringSystem.Application.Authentication.Commands.TalentRegister;

public class TalentRegisterCommandHandler : IRequestHandler<TalentRegisterCommand, ErrorOr<AuthenticationResponse>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly ITalentRepository _talentRepository;
    private readonly IPasswordHasher _passwordHasher;

    public TalentRegisterCommandHandler(IPasswordHasher passwordHasher, ITalentRepository talentRepository, IJwtTokenGenerator jwtTokenGenerator)
    {
        _passwordHasher = passwordHasher;
        _talentRepository = talentRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
    }


    public Task<ErrorOr<AuthenticationResponse>> Handle(TalentRegisterCommand talentRegister, CancellationToken cancellationToken)
    {
        if (ReferenceEquals(_talentRepository.GetTalentByEmail(talentRegister.Email), null) == false)
        {
            return Task.FromResult<ErrorOr<AuthenticationResponse>>(Errors.User.DuplicateEmail(talentRegister.Email));
        }
        
        string hashedPassword = _passwordHasher.HashPassword(talentRegister.Password);

        var talent = Talent.Create(
            name: talentRegister.Name,
            email: talentRegister.Email,
            password: hashedPassword,
            webSite: talentRegister.WebSite,
            about: talentRegister.About,
            profilePicture: talentRegister.ProfilePicture
        );
        
        _talentRepository.AddTalent(talent);

        var token = _jwtTokenGenerator.GenerateToken(talent.Id, talentRegister.Name, talentRegister.Email);

        return Task.FromResult<ErrorOr<AuthenticationResponse>>(new AuthenticationResponse(talent.Id, token));

    }
}