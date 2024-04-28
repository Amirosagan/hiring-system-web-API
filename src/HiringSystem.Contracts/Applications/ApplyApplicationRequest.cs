using Microsoft.AspNetCore.Http;

namespace HiringSystem.Contracts.Applications;

public record ApplyApplicationRequest(
    string JobId,
    IFormFile Resume,
    string? JobSeekerId,
    string? Supportive
);