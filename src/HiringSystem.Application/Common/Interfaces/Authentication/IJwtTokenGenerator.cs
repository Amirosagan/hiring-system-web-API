namespace HiringSystem.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    public string GenerateToken(Guid id, string name, string email);
}