namespace HiringSystem.Application.Common.Interfaces.Persistence;

public interface IApplicationRepository
{
    public Task<Domain.Application.Application?> GetByIdAsync(string id);
    public void Add(Domain.Application.Application application);
    public Task<List<Domain.Application.Application>> GetApplicationsAsync();
    public Task<List<Domain.Application.Application>> GetApplicationsWithJobIdAsync(string jobId);
}