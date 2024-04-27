using HiringSystem.Domain.Common.Models;

namespace HiringSystem.Domain.Application;

public sealed class Application : AggregateRoot<Guid>
{
    public Guid JobId { get; private set; }
    public Job.Job Job { get; private set; }
    public string Resume { get; private set; }
    public string Supportive { get; private set; }
    public DateTime CreatedAt { get; private set; }
    
    private Application(Guid id, Guid jobId, string resume, string supportive, DateTime createdAt) : base(id)
    {
        JobId = jobId;
        Resume = resume;
        Supportive = supportive;
        CreatedAt = createdAt;
    }
    
    public static Application Create(Guid jobId, string resume, string supportive)
    {
        return new Application(Guid.NewGuid(), jobId, resume, supportive, DateTime.Now);
    }
    
    #pragma warning disable CS8618
    private Application()
    { }
    #pragma warning restore CS8618
    
}