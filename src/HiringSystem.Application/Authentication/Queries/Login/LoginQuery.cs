using ErrorOr;
using HiringSystem.Application.Authentication.Common;
using MediatR;

namespace HiringSystem.Application.Authentication.Queries.Login;

public record LoginQuery(
    string Email,
    string Password
): IRequest<ErrorOr<AuthenticationResponse>>;