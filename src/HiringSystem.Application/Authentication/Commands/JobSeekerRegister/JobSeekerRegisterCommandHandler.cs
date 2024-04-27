using ErrorOr;

using HiringSystem.Application.Authentication.Common;
using HiringSystem.Application.Common.Interfaces.Authentication;
using HiringSystem.Application.Common.Interfaces.Persistence;
using HiringSystem.Application.Common.Interfaces.Services;
using HiringSystem.Domain.Common.Errors;
using HiringSystem.Domain.JobSeeker;
using HiringSystem.Domain.Talent;

using MediatR;

namespace HiringSystem.Application.Authentication.Commands.JobSeekerRegister;

public class JobSeekerRegisterCommandHandler : IRequestHandler<JobSeekerRegisterCommand, ErrorOr<AuthenticationResponse>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IJobSeekerRepository _jobSeekerRepository;
    private readonly IPasswordHasher _passwordHasher;

    public JobSeekerRegisterCommandHandler(IPasswordHasher passwordHasher, IJobSeekerRepository jobSeekerRepository, IJwtTokenGenerator jwtTokenGenerator)
    {
        _passwordHasher = passwordHasher;
        _jobSeekerRepository = jobSeekerRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
    }


    public async Task<ErrorOr<AuthenticationResponse>> Handle(JobSeekerRegisterCommand request, CancellationToken cancellationToken)
    {
        if (ReferenceEquals(await _jobSeekerRepository.GetJobSeekerByEmailAsync(request.Email), null) == false)
        {
            return Errors.User.DuplicateEmail(request.Email);
        }
        
        string hashedPassword = _passwordHasher.HashPassword(request.Password);

        var jobSeeker = JobSeeker.Create(
            name: request.Name,
            email: request.Email,
            password: hashedPassword,
            profilePicture: request.ProfilePicture,
            country: request.Country,
            title: request.Title
        );
        
        await _jobSeekerRepository.AddJobSeekerAsync(jobSeeker);

        var token = _jwtTokenGenerator.GenerateToken(jobSeeker.Id, request.Name, request.Email);

        return new AuthenticationResponse(jobSeeker.Id, token);
    }
}