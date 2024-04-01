using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HiringSystem.Application.Common.Interfaces.Authentication;
using HiringSystem.Application.Common.Interfaces.Services;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace HiringSystem.Infrastructure.Authentication;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly IDateTimeProvider _dateTimeProvider;
    
    private readonly JwtSettings _jwtSettings;

    public JwtTokenGenerator(IDateTimeProvider dateTimeProvider, IOptions<JwtSettings> jwtSettings)
    {
        _dateTimeProvider = dateTimeProvider;
        _jwtSettings = jwtSettings.Value;
    }
    public string GenerateToken(Guid id, string name, string email)
   {
       var signingCredentials = new SigningCredentials(
           new SymmetricSecurityKey(
               Encoding.UTF8.GetBytes(_jwtSettings.Key)
           ),
           SecurityAlgorithms.HmacSha256);
       
       var claims = new[]
       {
           new Claim(JwtRegisteredClaimNames.UniqueName, id.ToString()),
           new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
           new Claim(JwtRegisteredClaimNames.Name, name),
           new Claim(JwtRegisteredClaimNames.Email, email)
       };
       
      var token = new JwtSecurityToken(
          issuer: _jwtSettings.Issuer,
          claims: claims,
          expires: _dateTimeProvider.Now.AddHours(_jwtSettings.ExpiryHour), 
          signingCredentials: signingCredentials,
          audience: _jwtSettings.Audience
      );


      return new JwtSecurityTokenHandler().WriteToken(token);
   }
}