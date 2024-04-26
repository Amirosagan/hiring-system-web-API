using HiringSystem.Domain.Job.ValueObjects;

namespace HiringSystem.Contracts.Jobs;

public record GetJobsResponse(
    int Page,
    int PageSize,
    int Total,
    bool HasNext,
    bool HasPrevious,
    List<JobInList> Items
);

public record JobInList(
    string Id,
    string Title,
    string WorkPlace,
    string JobType,
    string TalentProfilePicture,
    string TalentName,
    DateTime CreatedAt
);
    
    