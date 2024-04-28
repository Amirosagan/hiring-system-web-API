using Microsoft.AspNetCore.Mvc;

namespace HiringSystem.Contracts.Applications;

public record GetApplicationsWithJobIdRequest(
    string? JobId,
    int? Page,
    int? PageSize
);