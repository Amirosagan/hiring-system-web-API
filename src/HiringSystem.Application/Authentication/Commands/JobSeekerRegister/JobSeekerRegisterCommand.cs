using ErrorOr;

using HiringSystem.Application.Authentication.Common;

using MediatR;

namespace HiringSystem.Application.Authentication.Commands.JobSeekerRegister;

public record JobSeekerRegisterCommand(
    string Name,
    string Email,
    string ProfilePicture,
    string Country,
    string Title,
    string Password
) : IRequest<ErrorOr<AuthenticationResponse>>;