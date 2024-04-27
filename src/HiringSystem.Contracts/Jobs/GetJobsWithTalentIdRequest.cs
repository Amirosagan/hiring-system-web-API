namespace HiringSystem.Contracts.Jobs;

public record GetJobsWithTalentIdRequest(
    int? Page,
    int? PageSize
);