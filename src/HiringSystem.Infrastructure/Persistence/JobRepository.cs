using HiringSystem.Application.Common.Interfaces.Persistence;
using HiringSystem.Domain.Job;

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
}