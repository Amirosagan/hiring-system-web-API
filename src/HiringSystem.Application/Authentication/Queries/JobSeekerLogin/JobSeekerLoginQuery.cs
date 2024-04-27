using ErrorOr;

using HiringSystem.Application.Authentication.Common;

using MediatR;

namespace HiringSystem.Application.Authentication.Queries.JobSeekerLogin;

public record JobSeekerLoginQuery(
    string Email,
    string Password
) : IRequest<ErrorOr<AuthenticationResponse>>;