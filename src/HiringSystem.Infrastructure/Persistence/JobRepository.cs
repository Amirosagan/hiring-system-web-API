using HiringSystem.Application.Common.Interfaces.Persistence;
using HiringSystem.Domain.Common.Models;
using HiringSystem.Domain.Job;
using HiringSystem.Domain.Job.ValueObjects;

using Microsoft.EntityFrameworkCore;

namespace HiringSystem.Infrastructure.Persistence;

public sealed class JobRepository : IJobRepository
{
    private readonly HiringSystemDbContext _dbContext;
    
    public JobRepository(HiringSystemDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public void AddJob(Job job)
    {
        _dbContext.Jobs.Add(job);
        _dbContext.SaveChanges();
    }
    
    public IQueryable<Job> GetJobsQueryable()
    {
        return _dbContext.Jobs;
    }

    public Job GetJobById(string requestJobId)
    {
        var jobIdQuery = Guid.Parse(requestJobId);
        return _dbContext.Jobs.FirstOrDefault(j => j.Id == jobIdQuery)!;
    }
    
    public bool JobExists(string jobId)
    {
        var jobIdQuery = Guid.Parse(jobId);
        return _dbContext.Jobs.Any(j=>j.Id == jobIdQuery);
    }
}