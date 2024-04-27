using HiringSystem.Domain.Common.Enums;
using HiringSystem.Domain.Job;

namespace HiringSystem.Contracts.Jobs;

public record GetJobResponse(
    string JobId,
    string Title,
    string Details,
    string TalentId,
    Salary Salary,
    string Country,
    WorkPlace WorkPlace,
    JobType JobType,
    string TalentJobUrl,
    DateTime CreatedAt
);