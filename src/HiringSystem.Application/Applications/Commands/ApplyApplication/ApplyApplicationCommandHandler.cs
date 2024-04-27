using ErrorOr;

using HiringSystem.Application.Common.Interfaces.Persistence;
using HiringSystem.Domain.Common.Errors;

using MediatR;

namespace HiringSystem.Application.Applications.Commands.ApplyApplication;

public class ApplyApplicationCommandHandler : IRequestHandler<ApplyApplicationCommand, ErrorOr<ApplyApplicationCommandResponse>>
{
    private readonly IApplicationRepository _applicationRepository;
    private readonly IJobSeekerRepository _jobSeekerRepository;
    private readonly IJobRepository _jobRepository;

    public ApplyApplicationCommandHandler(IApplicationRepository applicationRepository, IJobSeekerRepository jobSeekerRepository, IJobRepository jobRepository)
    {
        _applicationRepository = applicationRepository;
        _jobSeekerRepository = jobSeekerRepository;
        _jobRepository = jobRepository;
    }

    public async Task<ErrorOr<ApplyApplicationCommandResponse>> Handle(ApplyApplicationCommand request, CancellationToken cancellationToken)
    {
        if (ReferenceEquals(await _jobSeekerRepository.GetJobSeekerByIdAsync(request.JobSeekerId), null))
        {
            return Errors.Application.JobSeekerNotFound(request.JobSeekerId);
        }
        
        if (ReferenceEquals(_jobRepository.GetJobById(request.JobId), null))
        {
            return Errors.Job.JobNotFound(request.JobId);
        }
        
        var application =  Domain.Application.Application.Create(Guid.Parse(request.JobId), request.Resume, request.Supportive);
        
        _applicationRepository.Add(application);

        return new ApplyApplicationCommandResponse(application.Id.ToString());
    }
}