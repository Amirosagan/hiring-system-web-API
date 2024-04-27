using HiringSystem.Application.Authentication.Common;
using ErrorOr;

using HiringSystem.Application.Common.Interfaces.Authentication;
using HiringSystem.Application.Common.Interfaces.Persistence;
using HiringSystem.Application.Common.Interfaces.Services;
using HiringSystem.Domain.Common.Errors;

using MediatR;

namespace HiringSystem.Application.Authentication.Queries.JobSeekerLogin;

public class JobSeekerLoginQueryHandler :IRequestHandler<JobSeekerLoginQuery, ErrorOr<AuthenticationResponse>>
{
    private readonly IJobSeekerRepository _jobSeekerRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IPasswordHasher _passwordHasher;
    
    public JobSeekerLoginQueryHandler(IJobSeekerRepository jobSeekerRepository, IJwtTokenGenerator jwtTokenGenerator, IPasswordHasher passwordHasher)
    {
        _jobSeekerRepository = jobSeekerRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
        _passwordHasher = passwordHasher;
    }
    
    public async Task<ErrorOr<AuthenticationResponse>> Handle(JobSeekerLoginQuery request, CancellationToken cancellationToken)
    {
        var jobSeeker = await _jobSeekerRepository.GetJobSeekerByEmailAsync(request.Email);
        
        if (ReferenceEquals(jobSeeker, null))
        {
            return Errors.User.NotFound(request.Email);
        }
        
        if (!_passwordHasher.VerifyPassword(request.Password, jobSeeker.Password))
        {
            return Errors.User.InvalidPassword();
        }
        
        var token = _jwtTokenGenerator.GenerateToken(jobSeeker.Id, jobSeeker.Name, request.Email);
        
        return new AuthenticationResponse(jobSeeker.Id, token);
    }
}