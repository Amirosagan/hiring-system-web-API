namespace HiringSystem.Contracts.Authentication;

public record JobSeekerRegisterRequest(
    string Name,
    string Email,
    string ProfilePicture,
    string Country,
    string Title,
    string Password
);