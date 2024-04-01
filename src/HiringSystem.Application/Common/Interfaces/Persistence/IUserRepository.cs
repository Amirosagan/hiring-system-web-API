using HiringSystem.Domain.Entities;

namespace HiringSystem.Application.Common.Interfaces.Persistence;

public interface IUserRepository
{
    User? GetUserByEmail(string email);
    void AddUser(User user);
}