using HiringSystem.Domain.Job;

namespace HiringSystem.Application.Common.Interfaces.Persistence;

public interface IJobRepository
{
    void AddJob(Job job);
    
    IQueryable<Job> GetJobsQueryable();

    Job GetJobById(string requestJobId);
    bool JobExists(string jobId);
}
    