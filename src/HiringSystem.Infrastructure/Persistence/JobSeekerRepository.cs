using HiringSystem.Application.Common.Interfaces.Persistence;
using HiringSystem.Domain.JobSeeker;

using Microsoft.EntityFrameworkCore;

namespace HiringSystem.Infrastructure.Persistence;

public class JobSeekerRepository : IJobSeekerRepository
{
    private readonly HiringSystemDbContext _dbContext;

    public JobSeekerRepository(HiringSystemDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<JobSeeker?> GetJobSeekerByEmailAsync(string email)
    {
        return _dbContext.JobSeekers.FirstOrDefaultAsync(j => j.Email == email);
    }

    public Task<JobSeeker?> GetJobSeekerByIdAsync(string id)
    {
        return _dbContext.JobSeekers.FirstOrDefaultAsync(j=>j.Id == Guid.Parse(id)); 
    }

    public async Task AddJobSeekerAsync(JobSeeker jobSeeker)
    {
        _dbContext.JobSeekers.Add(jobSeeker);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateJobSeekerAsync(JobSeeker jobSeeker)
    {
        _dbContext.JobSeekers.Update(jobSeeker);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteJobSeekerAsync(JobSeeker jobSeeker)
    {
        _dbContext.JobSeekers.Remove(jobSeeker);
        await _dbContext.SaveChangesAsync();
    }

    public Task<List<JobSeeker>> GetJobSeekersAsync()
    {
        return _dbContext.JobSeekers.ToListAsync();
    }
}