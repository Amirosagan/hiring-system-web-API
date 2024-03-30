using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HiringSystem.Helpers;
using HiringSystem.Models;
using HiringSystem.Models.DTOs;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace HiringSystem.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly UserManager<Campany> _campanyManager;
    private readonly JWT _jwt;

    public AuthenticationController(UserManager<User> userManager, JWT jwt, UserManager<Campany> campanyManager)
    {
        _userManager = userManager;
        _jwt = jwt;
        _campanyManager = campanyManager;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegistrationRequestDto userRegistrationRequest)
    {
        var existingUser = await _userManager.FindByEmailAsync(userRegistrationRequest.Email);
        if (existingUser != null)
        {
            return BadRequest(new AuthResult()
            {
                Success = false,
                Errors = new List<string>() { "Email already in use" }
            });
        }

        var newUser = new User()
        {
            FirstName = userRegistrationRequest.FirstName,
            SecondName = userRegistrationRequest.SecondName,
            UserName = userRegistrationRequest.Email,
            Email = userRegistrationRequest.Email
        };
        var result = await _userManager.CreateAsync(newUser, userRegistrationRequest.Password);
        
        if (result.Succeeded)
        {
            // TODO: Generate the token
            return Ok(new AuthResult()
            {
                Success = true,
                Token = GenerateJwtToken(newUser)
            });
        }
        Console.WriteLine(result.Errors);
        
        return BadRequest(new AuthResult()
        {
            Success = false,
            Errors = result.Errors.Select(x => x.Description) 
        });
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginRequestDto userLoginRequest)
    {
        if (!ModelState.IsValid)
            return BadRequest(new AuthResult()
            {
                Success = false,
                Errors = new List<string>() { "Invalid payload" }
            });
        var existingUser = await _userManager.FindByEmailAsync(userLoginRequest.Email);
        if (existingUser == null)
        {
            return BadRequest(new AuthResult()
            {
                Success = false,
                Errors = new List<string>() { "Invalid login request" }
            });
        }

        var isCorrect = await _userManager.CheckPasswordAsync(existingUser, userLoginRequest.Password);
        if (!isCorrect)
        {
            return BadRequest(new AuthResult()
            {
                Success = false,
                Errors = new List<string>() { "Invalid login request" }
            });
        }
            
        return Ok(new AuthResult()
        {
            Success = true,
            Token = GenerateJwtToken(existingUser)
        });

    }
    
    
    private string GenerateJwtToken(User user)
    {
        var jwtToken = new JwtSecurityToken(
            issuer: _jwt.Issuer,
            audience: _jwt.Audience,
            claims: new List<Claim>(
                new[]
                {
                    new Claim("Id", user.Id),
                    new Claim("Name",user.FullName),
                    new Claim("Email", user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToUniversalTime().ToString(), ClaimValueTypes.Integer64)
                }
                ),
            expires: DateTime.Now.AddDays(_jwt.DurationInDays),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key)),
                SecurityAlgorithms.HmacSha256)
        );
        return new JwtSecurityTokenHandler().WriteToken(jwtToken);
    }

}