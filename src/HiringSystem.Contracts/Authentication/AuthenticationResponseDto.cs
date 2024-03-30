namespace HiringSystem.Contracts.Authentication;

public record AuthenticationResponseDto(
    Guid Id,
    string Token
);