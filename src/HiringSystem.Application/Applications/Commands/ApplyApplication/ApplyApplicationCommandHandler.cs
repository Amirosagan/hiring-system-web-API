using ErrorOr;

using HiringSystem.Application.Common.Interfaces.Persistence;
using HiringSystem.Application.Common.Interfaces.Storage;
using HiringSystem.Domain.Common.Errors;

using MediatR;

namespace HiringSystem.Application.Applications.Commands.ApplyApplication;

public class ApplyApplicationCommandHandler : IRequestHandler<ApplyApplicationCommand, ErrorOr<ApplyApplicationCommandResponse>>
{
    private readonly IApplicationRepository _applicationRepository;
    private readonly IJobSeekerRepository _jobSeekerRepository;
    private readonly IJobRepository _jobRepository;
    private readonly IDropbox _dropbox;

    public ApplyApplicationCommandHandler(IApplicationRepository applicationRepository, IJobSeekerRepository jobSeekerRepository, IJobRepository jobRepository, IDropbox dropbox)
    {
        _applicationRepository = applicationRepository;
        _jobSeekerRepository = jobSeekerRepository;
        _jobRepository = jobRepository;
        _dropbox = dropbox;
    }

    public async Task<ErrorOr<ApplyApplicationCommandResponse>> Handle(ApplyApplicationCommand request, CancellationToken cancellationToken)
    {
        var jobSeeker = await _jobSeekerRepository.GetJobSeekerByIdAsync(request.JobSeekerId);
        if (ReferenceEquals(jobSeeker, null))
        {
            return Errors.Application.JobSeekerNotFound(request.JobSeekerId);
        }
        
        if (ReferenceEquals(_jobRepository.GetJobById(request.JobId), null))
        {
            return Errors.Job.JobNotFound(request.JobId);
        }
        
        if (ReferenceEquals(request.Resume, null))
        {
            return Errors.Application.ResumeNotFound();
        }
        
        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(request.Resume.FileName)}";
        
        var url = await _dropbox.UploadAsync(fileName, request.Resume);
        
        var application = Domain.Application.Application.Create( Guid.Parse(request.JobId), Guid.Parse(request.JobSeekerId), jobSeeker, url, request.Supportive);
        
        _applicationRepository.Add(application);
        
        var result = new ApplyApplicationCommandResponse(application.Id.ToString());
        
        
        return result;
    }
}