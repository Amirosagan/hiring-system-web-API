using ErrorOr;

namespace HiringSystem.Domain.Common.Errors;

public static class Errors
{
    public static class User
    {
        public static Error DuplicateEmail(string email) => Error.Conflict("User.DuplicateEmail", $"User with email {email} already exists");
        public static Error NotFound(string email) => Error.Validation("User.Validation", $"Email or password is incorrect");
    }
}