using HiringSystem.Api.Filters;
using HiringSystem.Api.Middleware;
using HiringSystem.Application;
using HiringSystem.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

builder.Services.AddControllers(e => e.Filters.Add<ErrorHandlingFilterAttribute>());

var app = builder.Build();

// app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
