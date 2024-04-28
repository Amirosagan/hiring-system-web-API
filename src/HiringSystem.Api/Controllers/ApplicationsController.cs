using HiringSystem.Application.Applications.Commands.ApplyApplication;
using HiringSystem.Application.Applications.Queries.GetApplicationsWithJobId;
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
    public async Task<IActionResult> Apply([FromForm] ApplyApplicationRequest request, [FromForm] IFormFile resume)
    {
        var jobSeekerId = User.Identity?.Name;
        
        var dto = request with { JobSeekerId = jobSeekerId };

        var command = new ApplyApplicationCommand(dto.JobSeekerId,  dto.JobId, dto.Resume, dto.Supportive);
        
        var result = await _mediator.Send(command);
        
        return result.Match<IActionResult>(
            application =>
            {
                Console.WriteLine(application.ApplicationId);
                var response = _mapper.Map<ApplyApplicationResponse>(application);
                
                return Ok(response);
            } ,
            Problem
        );
    }
    
    [HttpGet]
    [Route("{jobId}")]
    [Authorize]
    
    public async Task<IActionResult> GetApplicationsWithJobId([FromQuery]GetApplicationsWithJobIdRequest request, [FromRoute]string jobId)
    {
        request = request with { JobId = jobId };
        
        var talentId = User.Identity?.Name;
        
        var query = _mapper.Map<GetApplicationsWithJobIdQuery>((request, talentId));
        
        var result = await _mediator.Send(query);
        
        return result.Match<IActionResult>(
            applications => Ok(_mapper.Map<GetApplicationsWithJobIdResponse>(applications)),
            Problem
        );
    }

    
}