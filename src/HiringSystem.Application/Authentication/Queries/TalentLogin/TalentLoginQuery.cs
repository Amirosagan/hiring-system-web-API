using ErrorOr;

using HiringSystem.Application.Authentication.Common;

using MediatR;

namespace HiringSystem.Application.Authentication.Queries.TalentLogin;

public record TalentLoginQuery(
    string Email,
    string Password
): IRequest<ErrorOr<AuthenticationResponse>>;