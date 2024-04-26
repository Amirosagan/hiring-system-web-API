using HiringSystem.Application.Common.Enum;
using HiringSystem.Domain.Common.Enums;

namespace HiringSystem.Contracts.Jobs;

public record GetJobsRequest(
    string? SearchTerm,
    WorkPlace? WorkPlace,
    JobType? JobType,
    string? SortWith,
    bool? Desc,
    int? Page,
    int? PageSize
);

   