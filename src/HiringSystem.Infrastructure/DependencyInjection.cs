using System.Text;
using HiringSystem.Application.Common.Interfaces.Authentication;
using HiringSystem.Application.Common.Interfaces.Persistence;
using HiringSystem.Application.Common.Interfaces.Services;
using HiringSystem.Application.Common.Interfaces.Storage;
using HiringSystem.Infrastructure.Authentication;
using HiringSystem.Infrastructure.Persistence;
using HiringSystem.Infrastructure.Services;
using HiringSystem.Infrastructure.Storage;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace HiringSystem.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = new JwtSettings();
        var dropboxSettings = new DropboxSettings();
        
        configuration.GetSection("Jwt").Bind(jwtSettings);
        configuration.GetSection("Dropbox").Bind(dropboxSettings);
        
        services.AddSingleton(Options.Create(jwtSettings));
        services.AddSingleton(Options.Create(dropboxSettings));
        
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddSingleton<IDropbox, Storage.Dropbox>();

        services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => options.TokenValidationParameters  = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
            });
        services.AddDbContext<HiringSystemDbContext>(option => option.UseSqlite("Data Source=../../local.db"));
        
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddSingleton<IUserRepository, UserRepository>();
        services.AddTransient<ITalentRepository, TalentRepository>();
        services.AddTransient<IApplicationRepository, ApplicationRepository>();
        services.AddTransient<IJobRepository, JobRepository>();
        services.AddTransient<IJobSeekerRepository, JobSeekerRepository>();
        services.AddSingleton<IPasswordHasher, PasswordHasher>();
        return services;
    }
}