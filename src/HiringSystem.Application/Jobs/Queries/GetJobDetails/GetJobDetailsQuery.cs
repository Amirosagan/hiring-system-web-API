using ErrorOr;

using HiringSystem.Domain.Job;

using MediatR;

namespace HiringSystem.Application.Jobs.Queries.GetJobDetails;

public record GetJobDetailsQuery(
    string JobId
) : IRequest<ErrorOr<Job>>;
    