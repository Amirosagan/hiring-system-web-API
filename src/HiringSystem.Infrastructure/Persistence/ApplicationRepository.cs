using HiringSystem.Application.Common.Interfaces.Persistence;

using Microsoft.EntityFrameworkCore;

namespace HiringSystem.Infrastructure.Persistence;

public class ApplicationRepository : IApplicationRepository
{
    private readonly HiringSystemDbContext _context;

    public ApplicationRepository(HiringSystemDbContext context)
    {
        _context = context;
    }

    public async Task<Domain.Application.Application?> GetByIdAsync(string id)
    {
        return await _context.Applications.FindAsync(Guid.Parse(id));
    }

    public void Add(Domain.Application.Application application)
    {
        _context.Applications.Add(application);
        _context.SaveChanges();
    }

    public async Task<List<Domain.Application.Application>> GetApplicationsAsync()
    {
        return (await _context.Applications.ToListAsync())!;
    }

    public async Task<List<Domain.Application.Application>> GetApplicationsWithJobIdAsync(string jobId)
    {
        return await _context.Applications
            .Where(a => a.JobId == Guid.Parse(jobId))
            .ToListAsync();
    }
}