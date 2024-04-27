using ErrorOr;

namespace HiringSystem.Domain.Common.Errors;

public static class Errors
{
    public static class User
    {
        public static Error DuplicateEmail(string email) => Error.Conflict("User.DuplicateEmail", $"User with email {email} already exists");
        public static Error NotFound(string email) => Error.Validation("User.Validation", $"Email or password is incorrect");
        public static Error InvalidPassword() => Error.Validation("User.Validation", "Email or password is incorrect");
    }
    public static class Job
    {
        public static Error JobNotFound(string jobId) => Error.NotFound("Job.NotFound", $"Job with id {jobId} not found");
        public static Error JobAlreadyExists(string jobId) => Error.Conflict("Job.AlreadyExists", $"Job with id {jobId} already exists");
        public static Error TalentNotFound(string talentId) => Error.NotFound("Talent.NotFound", $"Talent with id {talentId} not found");
    }

    public static class Application
    {
        public static Error ApplicationNotFound(string applicationId) => Error.NotFound("Application.NotFound", $"Application with id {applicationId} not found");
        public static Error ApplicationAlreadyExists(string applicationId) => Error.Conflict("Application.AlreadyExists", $"Application with id {applicationId} already exists");
        public static Error JobSeekerNotFound(string jobSeekerId) => Error.NotFound("JobSeeker.NotFound", $"JobSeeker with id {jobSeekerId} not found");
        
    }
}