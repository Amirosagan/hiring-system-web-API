using ErrorOr;

using HiringSystem.Application.Common.Interfaces.Persistence;
using HiringSystem.Domain.Common.Errors;

using MediatR;

namespace HiringSystem.Application.Applications.Queries.GetApplicationsWithJobId;

public class GetApplicationsWithJobQueryHandler : IRequestHandler<GetApplicationsWithJobIdQuery, ErrorOr<PagedApplicationsList>>
{
    private readonly ITalentRepository _talentRepository;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IJobRepository _jobRepository;

    public GetApplicationsWithJobQueryHandler(ITalentRepository talentRepository, IApplicationRepository applicationRepository, IJobRepository jobRepository)
    {
        _talentRepository = talentRepository;
        _applicationRepository = applicationRepository;
        _jobRepository = jobRepository;
    }

    public async Task<ErrorOr<PagedApplicationsList>> Handle(GetApplicationsWithJobIdQuery request, CancellationToken cancellationToken)
    {
        if (!_talentRepository.ExistsWithId(request.TalentId))
        {
            return Errors.Job.TalentNotFound(request.TalentId);
        }

        var job = _jobRepository.GetJobById(request.JobId);
        
        if (ReferenceEquals(job, null))
        {
            return Errors.Job.JobNotFound(request.JobId);
        }
        
        if(job.TalentId.ToString() != request.TalentId)
        {
            return Errors.Job.JobNotOwnedByTalent(request.JobId, request.TalentId); 
        }
        
        var applications = _applicationRepository.GetApplicationWithJobIdQuery(request.JobId);
        
        var pagedApplication = await PagedApplicationsList.CreateAsync(applications, request.Page ?? 1, request.PageSize ?? 10);
        
        return pagedApplication;
    }
}