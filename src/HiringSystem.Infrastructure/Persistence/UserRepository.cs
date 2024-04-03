using HiringSystem.Application.Common.Interfaces.Persistence;
using HiringSystem.Domain.Entities;

namespace HiringSystem.Infrastructure.Persistence;

public class UserRepository : IUserRepository
{
    private readonly List<User> _users = []; 
    public User? GetUserByEmail(string email)
    {
        return _users.FirstOrDefault(u => u.Email == email);
    }

    public void AddUser(User user)
    {
        _users.Add(user);
    }
}