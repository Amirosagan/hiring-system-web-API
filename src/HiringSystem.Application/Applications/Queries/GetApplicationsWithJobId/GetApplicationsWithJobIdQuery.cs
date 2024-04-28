using MediatR;
using ErrorOr;

namespace HiringSystem.Application.Applications.Queries.GetApplicationsWithJobId;

public record GetApplicationsWithJobIdQuery(
    string TalentId,
    string JobId,
    int? Page,
    int? PageSize
): IRequest<ErrorOr<PagedApplicationsList>>;