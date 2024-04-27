using HiringSystem.Application.Jobs.Common;
using ErrorOr;

using MediatR;

namespace HiringSystem.Application.Jobs.Queries.GetJobsWithTalentId;

public record GetJobsWithTalentIdQuery(string TalentId,
    int? Page,
    int? PageSize
) : IRequest<ErrorOr<PagedJobList>>;