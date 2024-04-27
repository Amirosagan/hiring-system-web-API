using HiringSystem.Application.Authentication.Commands.JobSeekerRegister;
using HiringSystem.Application.Authentication.Commands.TalentRegister;
using HiringSystem.Application.Authentication.Queries.JobSeekerLogin;
using HiringSystem.Application.Authentication.Queries.TalentLogin;
using HiringSystem.Contracts.Authentication;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HiringSystem.Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    [AllowAnonymous]
    public class AuthenticationController : ErrorApiController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public AuthenticationController( IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [Route("Talent/register")]
        [HttpPost]
        public async Task<IActionResult> Register( TalentRegisterRequestDto request)
        {
            var command = _mapper.Map<TalentRegisterCommand>(request);
            
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
        [Route("Talent/login")]
        [HttpPost]
        public async Task<IActionResult> TalentLogin(LoginRequestDto request)
        {
            var query = _mapper.Map<TalentLoginQuery>(request);
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
        
        [Route("JobSeeker/register")]
        [HttpPost]
        public async Task<IActionResult> Register(JobSeekerRegisterRequest request)
        {
            var command = _mapper.Map<JobSeekerRegisterCommand>(request);
            
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

        [Route("JobSeeker/login")]
        [HttpPost]
        public async Task<IActionResult> JobSeekerLogin(LoginRequestDto request)
        {
            var query = _mapper.Map<JobSeekerLoginQuery>(request);
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
