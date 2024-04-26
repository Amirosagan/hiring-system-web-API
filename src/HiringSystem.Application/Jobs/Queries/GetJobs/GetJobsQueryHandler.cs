using System.Linq.Expressions;

using ErrorOr;

using HiringSystem.Application.Common.Helper;
using HiringSystem.Application.Common.Interfaces.Persistence;
using HiringSystem.Domain.Job;
using HiringSystem.Domain.Job.ValueObjects;
using HiringSystem.Domain.Talent;

using MediatR;

namespace HiringSystem.Application.Jobs.Queries.GetJobs;

public class GetJobsQueryHandler : IRequestHandler<GetJobsQuery, ErrorOr<PagedJobList>>
{
    private readonly IJobRepository _jobRepository;

    public GetJobsQueryHandler(IJobRepository jobRepository) 
    {
        _jobRepository = jobRepository;
    }

    public Task<ErrorOr<PagedJobList>> Handle(GetJobsQuery request, CancellationToken cancellationToken)
    {
        var jobs = _jobRepository.GetJobsQueryable();
        
        if (!string.IsNullOrEmpty(request.SearchTerm))
        {
            jobs = jobs.Where(x => x.Title.Contains(request.SearchTerm) || x.Details.Contains(request.SearchTerm));
        }
        if (request.WorkPlace.HasValue)
        {
            jobs = jobs.Where(x => x.WorkPlace == request.WorkPlace);
        }
        if (request.JobType.HasValue)
        {
            jobs = jobs.Where(x => x.JobType == request.JobType);
        }

        Expression<Func<Job, object>> sortExpression = request.SortWith?.ToLower() switch
        {
            "salary" => x => x.Salary.Maximum,
            _ => x => x.CreatedAt
        };
        
        if ((request.Desc.HasValue && request.Desc.Value) || request.SortWith is null)
        {
            jobs = jobs.OrderByDescending(sortExpression);
        }
        else
        {
            jobs = jobs.OrderBy(sortExpression);
        }
        
        var pagedJobs= PagedJobList.CreateAsync(jobs, request.Page ?? 1, request.PageSize ?? 10).Result;
        
        return Task.FromResult<ErrorOr<PagedJobList>>(pagedJobs);
    }
}