using System.Reflection;
using HiringSystem.Application.Authentication.Commands.Register;
using HiringSystem.Application.Authentication.Common;
using HiringSystem.Application.Common.Behaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ErrorOr;
using FluentValidation;

namespace HiringSystem.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        
        return services;
    }
}