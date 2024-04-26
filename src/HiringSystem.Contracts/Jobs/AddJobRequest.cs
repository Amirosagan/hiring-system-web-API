using HiringSystem.Domain.Common.Enums;

namespace HiringSystem.Contracts.Jobs;

public record AddJobRequest(
    string Title,
    string Description,
    WorkPlace WorkPlace,
    string Country,
    JobType JobType,
    Salary Salary,
    string TalentJobUrl
);

public record Salary(decimal Min, decimal Max, string Currency, Period Period);