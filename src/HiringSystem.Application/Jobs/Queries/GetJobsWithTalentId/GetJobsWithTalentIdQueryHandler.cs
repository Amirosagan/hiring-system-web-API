using ErrorOr;

using HiringSystem.Application.Common.Interfaces.Persistence;
using HiringSystem.Application.Jobs.Common;
using HiringSystem.Domain.Common.Errors;

using MediatR;

namespace HiringSystem.Application.Jobs.Queries.GetJobsWithTalentId;

public class GetJobsWithTalentIdQueryHandler : IRequestHandler<GetJobsWithTalentIdQuery, ErrorOr<PagedJobList>>
{
    private readonly IJobRepository _jobRepository;
    private readonly ITalentRepository _talentRepository;

    public GetJobsWithTalentIdQueryHandler(IJobRepository jobRepository, ITalentRepository talentRepository)
    {
        _jobRepository = jobRepository;
        _talentRepository = talentRepository;
    }

    public async Task<ErrorOr<PagedJobList>> Handle(GetJobsWithTalentIdQuery request, CancellationToken cancellationToken)
    {
        if(!_talentRepository.ExistsWithId(request.TalentId))
        {
            return Errors.Job.TalentNotFound(request.TalentId);
        }
        
        var jobs = _jobRepository.GetJobsQueryable();

        jobs = jobs.Where(j => j.TalentId == Guid.Parse(request.TalentId));
        
        var pagedJobs = PagedJobList.CreateAsync(jobs, request.Page ?? 1, request.PageSize ?? 10).Result;
        
        return pagedJobs;
    }
}