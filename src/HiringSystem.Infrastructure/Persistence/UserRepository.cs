using HiringSystem.Application.Common.Interfaces.Persistence;
using HiringSystem.Domain.Entities;

namespace HiringSystem.Infrastructure.Persistence;

public class UserRepository : IUserRepository
{
    private static readonly List<User> Users = []; 
    public User? GetUserByEmail(string email)
    {
        return Users.FirstOrDefault(u => u.Email == email);
    }

    public void AddUser(User user)
    {
        Users.Add(user);
    }
}