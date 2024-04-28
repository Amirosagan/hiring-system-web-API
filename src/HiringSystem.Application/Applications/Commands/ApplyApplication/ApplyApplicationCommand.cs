using ErrorOr;

using MediatR;

using Microsoft.AspNetCore.Http;

namespace HiringSystem.Application.Applications.Commands.ApplyApplication;

public record ApplyApplicationCommand(
    string JobSeekerId,
    string JobId,
    IFormFile Resume,
    string Supportive
) : IRequest<ErrorOr<ApplyApplicationCommandResponse>>;