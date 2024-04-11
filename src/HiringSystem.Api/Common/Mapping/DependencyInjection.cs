using System.Reflection;
using Mapster;
using MapsterMapper;

namespace HiringSystem.Api.Common.Mapping;

public static class DependencyInjection
{
    public static void AddMapping(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());
        services.AddSingleton(config);
        services.AddSingleton<IMapper, ServiceMapper>();
    }
}