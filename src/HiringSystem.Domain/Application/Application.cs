using HiringSystem.Domain.Common.Models;

namespace HiringSystem.Domain.Application;

public sealed class Application : AggregateRoot<Guid>
{
    public Guid JobId { get; private set; }
    public Job.Job Job { get; private set; }
    public Guid JobSeekerId { get; private set; }
    public string Resume { get; private set; }
    public string Supportive { get; private set; }
    public JobSeeker.JobSeeker JobSeeker { get; private set; }
    public DateTime CreatedAt { get; private set; }
    
    
    private Application(Guid id, Guid jobId, Guid jobSeekerId, string resume, string supportive, DateTime createdAt, JobSeeker.JobSeeker jobSeeker) : base(id)
    {
        JobId = jobId;
        Resume = resume;
        Supportive = supportive;
        CreatedAt = createdAt;
        JobSeeker = jobSeeker;
        JobSeekerId = jobSeekerId;
    }
    
    public static Application Create(Guid jobId, Guid jobSeekerId,JobSeeker.JobSeeker jobSeeker, string resume, string supportive)
    {
        return new Application(Guid.NewGuid(), jobId, jobSeekerId, resume, supportive, DateTime.UtcNow, jobSeeker);
    }
    
    #pragma warning disable CS8618
    private Application()
    { }
    #pragma warning restore CS8618
    
}