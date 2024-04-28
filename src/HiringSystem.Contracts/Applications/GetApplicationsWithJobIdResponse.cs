namespace HiringSystem.Contracts.Applications;

public record GetApplicationsWithJobIdResponse(
    
    int Page,
    int PageSize,
    int Total,
    bool HasNext,
    bool HasPrevious,
    List<ApplicationInList> Items
);

public record ApplicationInList(
    string Id,
    string Resume,
    string Supportive,
    string JobId,
    DateTime CreatedAt
);