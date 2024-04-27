using HiringSystem.Domain.JobSeeker;

namespace HiringSystem.Application.Common.Interfaces.Persistence;

public interface IJobSeekerRepository
{
    Task<JobSeeker?> GetJobSeekerByEmailAsync(string email);
    Task<JobSeeker?> GetJobSeekerByIdAsync(string id);
    Task AddJobSeekerAsync(JobSeeker jobSeeker);
    Task UpdateJobSeekerAsync(JobSeeker jobSeeker);
    Task DeleteJobSeekerAsync(JobSeeker jobSeeker);
    Task<List<JobSeeker>> GetJobSeekersAsync();
}