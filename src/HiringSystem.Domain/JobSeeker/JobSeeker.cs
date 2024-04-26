using HiringSystem.Domain.Common.Models;
using HiringSystem.Domain.JobSeeker.ValueObjects;

namespace HiringSystem.Domain.JobSeeker;

public sealed class JobSeeker : AggregateRoot<JobSeekerId, Guid>
{
    public string Name { get; }
    public string Email { get; }
    public string ProfilePicture { get; }
    public string Country { get; }
    public string Title { get; }
    
    private JobSeeker(JobSeekerId id, string name, string email, string profilePicture, string country, string title) : base(id)
    {
        Name = name;
        Email = email;
        ProfilePicture = profilePicture;
        Country = country;
        Title = title;
    }
    
    public static JobSeeker Create(string name, string email, string profilePicture, string country, string title)
    {
        return new JobSeeker(JobSeekerId.Create(), name, email, profilePicture, country, title);
    }
}