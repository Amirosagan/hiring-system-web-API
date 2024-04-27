namespace HiringSystem.Contracts.Authentication;

public record TalentRegisterRequestDto(
    string Name,
    string Email,
    string Password,
    string WebSite,
    string About,
    string ProfilePicture
);
