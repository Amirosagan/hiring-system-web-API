using HiringSystem.Api.Errors;
using HiringSystem.Api.Filters;
using HiringSystem.Api.Middleware;
using HiringSystem.Application;
using HiringSystem.Infrastructure;
using Microsoft.AspNetCore.Mvc.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddSingleton<ProblemDetailsFactory, SystemProblemDetailsFactory>();

var app = builder.Build();

// app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseExceptionHandler("/error");

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
