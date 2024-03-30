using HiringSystem.Application.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace HiringSystem.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        return services;
    }
}