using HiringSystem.Api;
using HiringSystem.Application;
using HiringSystem.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddPresentation()
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

var app = builder.Build();

// app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseExceptionHandler("/error");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
