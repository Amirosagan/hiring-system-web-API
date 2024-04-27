using System.Security.Cryptography;

using HiringSystem.Application.Common.Interfaces.Services;

namespace HiringSystem.Infrastructure.Services;

public class PasswordHasher : IPasswordHasher
{
    private const int SaltSize = 16;
    
    private const int KeySize = 32;
    
    private const int Iterations = 10000;
    
    private static readonly HashAlgorithmName HashAlgorithmName = HashAlgorithmName.SHA256;
    
    private const char Delimiter = '.';
    
    public string HashPassword(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(SaltSize);
        var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, HashAlgorithmName, KeySize);

        // Console.WriteLine($"{Convert.ToBase64String(salt)}{Delimiter}{Convert.ToBase64String(hash)}");

        return $"{Convert.ToBase64String(salt)}{Delimiter}{Convert.ToBase64String(hash)}";
    }

    public bool VerifyPassword(string password, string hashedPassword)
    {
        var parts = hashedPassword.Split(Delimiter);
        var salt = Convert.FromBase64String(parts[0]);
        var hash = Convert.FromBase64String(parts[1]);

        var newHash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, HashAlgorithmName, KeySize);

        return newHash.SequenceEqual(hash);
    }
}