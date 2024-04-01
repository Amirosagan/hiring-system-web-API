using HiringSystem.Application.Authentication;
using HiringSystem.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace HiringSystem.Api.Contollers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> Register( UserRegisterRequestDto request)
        {
            var authResponse = await _authenticationService.Register(request.Name, request.Email, request.Password);

            var response = new AuthenticationResponseDto(
                authResponse.Id,
                authResponse.Token
            );

            return Ok(response);
        }
        [Route("login")]
        [HttpPost]
        public  async Task<IActionResult> Login( LoginRequestDto request)
        {
            var authResponse = await _authenticationService.Login(request.Email, request.Password);

            var response = new AuthenticationResponseDto(
                authResponse.Id,
                authResponse.Token
            );

            return Ok(response);
        }
    }
}
