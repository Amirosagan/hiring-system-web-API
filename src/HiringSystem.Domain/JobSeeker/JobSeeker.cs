using HiringSystem.Domain.Common.Models;

namespace HiringSystem.Domain.JobSeeker;

public sealed class JobSeeker : AggregateRoot<Guid>
{
    public string Name { get; }
    public string Email { get; }
    public string ProfilePicture { get; }
    public string Country { get; }
    public string Title { get; }
    public string Password { get; private set; }
    
    private JobSeeker(Guid id, string name, string email, string profilePicture, string country, string title, string password) : base(id)
    {
        Name = name;
        Email = email;
        ProfilePicture = profilePicture;
        Country = country;
        Title = title;
        Password = password;
    }
    
    public static JobSeeker Create(string name, string email, string profilePicture, string country, string title, string password)
    {
        return new JobSeeker(Guid.NewGuid(), name, email, profilePicture, country, title, password);
    }
}