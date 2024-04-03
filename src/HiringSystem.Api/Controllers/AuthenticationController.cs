using HiringSystem.Application.Authentication.Commands.Register;
using HiringSystem.Application.Authentication.Queries.Login;
using HiringSystem.Contracts.Authentication;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HiringSystem.Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ErrorApiController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public AuthenticationController( IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> Register( UserRegisterRequestDto request)
        {
            var command = _mapper.Map<RegisterCommand>(request);
            
            var authResponse = await _mediator.Send(command);
            
            return authResponse.Match<IActionResult>(
                success =>
                {
                    var response = _mapper.Map<AuthenticationResponseDto>(success);
                    
                    return Ok(response);
                },
                Problem
            );

        }
        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDto request)
        {
            var query = _mapper.Map<LoginQuery>(request);
            var authResponse = await _mediator.Send(query);
            
            return authResponse.Match<IActionResult>(
                success =>
                {
                    var response = _mapper.Map<AuthenticationResponseDto>(success);
                    
                    return Ok(response);
                },
                Problem
            );

        }
    }
}
