using HiringSystem.Domain.Common.Enums;
using HiringSystem.Domain.Common.Models;
using HiringSystem.Domain.Job.ValueObjects;
using HiringSystem.Domain.Talent.ValueObjects;

using ApplicationId = HiringSystem.Domain.Application.ValueObjects.ApplicationId;

namespace HiringSystem.Domain.Job;

public sealed class Job : AggregateRoot<JobId, Guid>
{
    public AggregateRootId<Guid> TalentId { get; private set; }
    public Talent.Talent Talent { get; private set; }
    public string Title{ get; private set; }
    public string Details { get; private set; }
    public JobType JobType { get; private set; }
    public WorkPlace WorkPlace { get; private set; }
    public string Country { get; private set; }
    public Salary Salary { get; private set; }
    public string TalentJobUrl { get; private set; }
    
    public DateTime CreatedAt { get; private set; }

    public IReadOnlyList<Application.Application>? Applications { get; private set; }

    public Job(JobId id, TalentId talentId, string title, string details, JobType jobType, WorkPlace workPlace, string country, Salary salary, DateTime createdAt, string talentJobUrl, Talent.Talent talent) : base(id)
    {
        TalentId = talentId;
        Title = title;
        Details = details;
        JobType = jobType;
        WorkPlace = workPlace;
        Country = country;
        Salary = salary;
        CreatedAt = createdAt;
        TalentJobUrl = talentJobUrl;
        Talent = talent;
        Applications = [];
    }
    
    public static Job Create(TalentId talentId, Talent.Talent talent, string title, string details, JobType jobType, WorkPlace workPlace, string country, Salary salary, string talentJobUrl)
    {
        return new Job(JobId.Create(), talentId, title, details, jobType, workPlace, country, salary, DateTime.Now, talentJobUrl, talent);
    }
    #pragma warning disable CS8618
    private Job()
    { }
    #pragma warning restore CS8618

}