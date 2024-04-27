using ErrorOr;

using HiringSystem.Application.Jobs.Common;
using HiringSystem.Domain.Common.Enums;

using MediatR;


namespace HiringSystem.Application.Jobs.Queries.GetJobs;

public record GetJobsQuery(
    string? SearchTerm,
    WorkPlace? WorkPlace,
    JobType? JobType,
    string? SortWith,
    bool? Desc,
    int? Page,
    int? PageSize
): IRequest<ErrorOr<PagedJobList>>;

   
