using HiringSystem.Domain.Job;
using ErrorOr;

using HiringSystem.Application.Common.Interfaces.Persistence;
using HiringSystem.Domain.Common.Errors;

using MediatR;

namespace HiringSystem.Application.Jobs.Queries.GetJobDetails;

public class GetJobDetailsQueryHandler : IRequestHandler<GetJobDetailsQuery, ErrorOr<Job>>
{
    private readonly IJobRepository _jobRepository;

    public GetJobDetailsQueryHandler(IJobRepository jobRepository)
    {
        _jobRepository = jobRepository;
    }

    public Task<ErrorOr<Job>> Handle(GetJobDetailsQuery request, CancellationToken cancellationToken)
    {

        if (!_jobRepository.JobExists(request.JobId))
        {
            return Task.FromResult<ErrorOr<Job>>(Errors.Job.JobAlreadyExists(request.JobId));
        }
        
        var job = _jobRepository.GetJobById(request.JobId);
        
        return Task.FromResult<ErrorOr<Job>>(job);
    }
}