using HiringSystem.Application.Common.Helper;
using HiringSystem.Domain.Job.ValueObjects;

namespace HiringSystem.Application.Jobs.Queries.GetJobs;

public record JobInList(
    string Id,
    string Title,
    string WorkPlace,
    string JobType,
    string TalentProfilePicture,
    string TalentName,
    DateTime CreatedAt
);