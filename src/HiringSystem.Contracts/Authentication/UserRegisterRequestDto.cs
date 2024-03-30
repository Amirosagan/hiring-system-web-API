namespace HiringSystem.Contracts.Authentication;

public record UserRegisterRequestDto(
    string Name,
    string Email,
    string Password
);
