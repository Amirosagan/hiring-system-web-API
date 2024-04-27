using ErrorOr;

using HiringSystem.Application.Authentication.Common;

using MediatR;

namespace HiringSystem.Application.Authentication.Commands.TalentRegister;

public record TalentRegisterCommand(
    string Name,
    string Email,
    string Password,
    string WebSite,
    string About,
    string ProfilePicture
): IRequest<ErrorOr<AuthenticationResponse>>;