using HiringSystem.Domain.Common.Models;
using HiringSystem.Domain.Job.ValueObjects;
using HiringSystem.Domain.Talent.ValueObjects;
using ApplicationId = HiringSystem.Domain.Application.ValueObjects.ApplicationId;

namespace HiringSystem.Domain.Application;

public sealed class Application : AggregateRoot<ApplicationId, Guid>
{
    public AggregateRootId<Guid> JobId { get; private set; }
    public Job.Job Job { get; private set; }
    public string Resume { get; private set; }
    public string Supportive { get; private set; }
    public DateTime CreatedAt { get; private set; }
    
    private Application(ApplicationId id, JobId jobId, string resume, string supportive, DateTime createdAt, Job.Job job) : base(id)
    {
        JobId = jobId;
        Resume = resume;
        Supportive = supportive;
        CreatedAt = createdAt;
        Job = job;
    }
    
    public static Application Create(JobId jobId, string resume, string supportive, Job.Job job)
    {
        return new Application(ApplicationId.Create(), jobId, resume, supportive, DateTime.Now, job);
    }
    
    #pragma warning disable CS8618
    private Application()
    { }
    #pragma warning restore CS8618
    
}