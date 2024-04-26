using HiringSystem.Domain.Common.Enums;
using HiringSystem.Domain.Job;
using ErrorOr;

using MediatR;

namespace HiringSystem.Application.Jobs.Commands.AddJob;

public record AddJobCommand(
    string Title,
    string Description,
    WorkPlace WorkPlace,
    string Country,
    JobType JobType,
    SalaryCommand Salary,
    string TalentJobUrl,
    string TalentId
) : IRequest<ErrorOr<Job>>;

public record SalaryCommand(decimal Min, decimal Max, string Currency, Period Period);