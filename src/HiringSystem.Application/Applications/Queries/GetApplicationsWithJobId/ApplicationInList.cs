namespace HiringSystem.Application.Applications.Queries.GetApplicationsWithJobId;

public record ApplicationInList(
    string ApplicationId,
    string Resume,
    string Supportive,
    string JobId,
    DateTime CreatedAt
);