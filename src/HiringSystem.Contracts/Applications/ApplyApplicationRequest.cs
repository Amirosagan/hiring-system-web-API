namespace HiringSystem.Contracts.Applications;

public record ApplyApplicationRequest(
    string JobId,
    string Resume,
    string Supportive
);