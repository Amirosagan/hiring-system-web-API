using HiringSystem.Api.Common.Mapping;
using HiringSystem.Api.Errors;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace HiringSystem.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        
        services.AddMapping();

        services.AddControllers();

        services.AddSingleton<ProblemDetailsFactory, SystemProblemDetailsFactory>();
        
        return services;
    }
    
}