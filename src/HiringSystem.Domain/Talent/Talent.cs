using HiringSystem.Domain.Common.Models;
using HiringSystem.Domain.Talent.ValueObjects;
using HiringSystem.Domain.Job;
using HiringSystem.Domain.JobSeeker.ValueObjects;

namespace HiringSystem.Domain.Talent;

public sealed class Talent : AggregateRoot<TalentId, Guid>
{
    public string Name { get; }
    public string Email { get; }
    public string WebSite { get; }
    public string About { get; private set; }
    public string ProfilePicture { get; private set; }
    public List<Job.Job>? Jobs { get; } 
    
    public string Password { get; private set; }

    private Talent(TalentId id, string name, string email, string webSite, string about, string profilePicture, List<Job.Job> jobs, string password) : base(id)
    {
        Name = name;
        Email = email;
        WebSite = webSite;
        About = about;
        ProfilePicture = profilePicture;
        Jobs = jobs;
        Password = password;
    }
    
    public static Talent Create(string name, string email, string webSite, string about, string profilePicture, string password)
    {
        return new Talent(TalentId.Create(), name, email, webSite, about, profilePicture, [], password);
    }
   #pragma warning disable CS8618
    private Talent() : base()
    { }
    #pragma warning restore CS8618
}