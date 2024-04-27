using HiringSystem.Application.Applications.Commands.ApplyApplication;
using HiringSystem.Contracts.Applications;

using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HiringSystem.Api.Controllers;

[ApiController]
[Route("api/applications")]
public class ApplicationsController : ErrorApiController
{
    private readonly IMapper _mapper;
    private readonly ISender _mediator;

    public ApplicationsController(IMapper mapper, ISender mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }
    
    [HttpPost]
    [Route("apply")]
    [Authorize]
    public async Task<IActionResult> Apply(ApplyApplicationRequest request)
    {
        var jobSeekerId = User.Identity?.Name;
        
        var command = _mapper.Map<ApplyApplicationCommand>((request, jobSeekerId));
        
        var result = await _mediator.Send(command);
        
        return result.Match<IActionResult>(
            application => Ok(_mapper.Map<ApplyApplicationResponse>(application)),
            Problem
        );
    }

}