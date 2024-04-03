using ErrorOr;
using HiringSystem.Application.Authentication.Common;
using MediatR;

namespace HiringSystem.Application.Authentication.Commands.Register;

public record RegisterCommand(
    string Name,
    string Email,
    string Password
): IRequest<ErrorOr<AuthenticationResponse>>;