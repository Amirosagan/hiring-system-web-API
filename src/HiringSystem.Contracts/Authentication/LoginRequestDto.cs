namespace HiringSystem.Contracts.Authentication;

public record LoginRequestDto(
    string Email,
    string Password
);
