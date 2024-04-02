using HiringSystem.Application.Authentication;
using HiringSystem.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace HiringSystem.Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ErrorApiController
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [Route("register")]
        [HttpPost]
        public IActionResult Register( UserRegisterRequestDto request)
        {
            var authResponse = _authenticationService.Register(request.Name, request.Email, request.Password);
            
            return authResponse.Match<IActionResult>(
                success =>
                {  
                    var result = success.Result;
                    var response = new AuthenticationResponseDto(
                        result.Id,
                        result.Token
                    );
                    
                    return Ok(response);
                },
                Problem
            );

        }
        [Route("login")]
        [HttpPost]
        public  IActionResult Login( LoginRequestDto request)
        {
            var authResponse = _authenticationService.Login(request.Email, request.Password);
            
            return authResponse.Match<IActionResult>(
                success =>
                {
                    var result = success.Result;
                    var response = new AuthenticationResponseDto(
                        result.Id,
                        result.Token
                    );
                    
                    return Ok(response);
                },
                Problem
            );

        }
    }
}
