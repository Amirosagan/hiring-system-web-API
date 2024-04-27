using ErrorOr;

using MediatR;

namespace HiringSystem.Application.Applications.Commands.ApplyApplication;

public record ApplyApplicationCommand(
    string JobSeekerId,
    string JobId,
    string Resume,
    string Supportive
) : IRequest<ErrorOr<ApplyApplicationCommandResponse>>;