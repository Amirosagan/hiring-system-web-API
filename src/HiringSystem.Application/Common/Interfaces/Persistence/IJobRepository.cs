using HiringSystem.Domain.Job;

namespace HiringSystem.Application.Common.Interfaces.Persistence;

public interface IJobRepository
{
    void AddJob(Job job);
    
    IQueryable<Job> GetJobsQueryable();
    
}
    