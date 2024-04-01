using HiringSystem.Application.Common.Interfaces.Services;

namespace HiringSystem.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime Now => DateTime.UtcNow;
}