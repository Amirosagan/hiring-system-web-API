using HiringSystem.Application.Jobs.Commands.AddJob;
using HiringSystem.Application.Jobs.Queries.GetJobDetails;
using HiringSystem.Application.Jobs.Queries.GetJobs;
using HiringSystem.Application.Jobs.Queries.GetJobsWithTalentId;
using HiringSystem.Contracts.Jobs;

using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using GetJobsResponse = HiringSystem.Contracts.Jobs.GetJobsResponse;

namespace HiringSystem.Api.Controllers;

[ApiController]
[Route("api/jobs")]
public class JobsController : ErrorApiController
{
    private readonly IMapper _mapper;
    private readonly ISender _mediator;

    public JobsController(IMapper mapper, ISender mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetJobs([FromQuery]GetJobsRequest request)
    {
        var query = _mapper.Map<GetJobsQuery>(request);

        var result = await _mediator.Send(query);

        return result.Match<IActionResult>(
            jobs => Ok(_mapper.Map<GetJobsResponse>(jobs)),
            Problem
        );
    }

    [Authorize]
    public async Task<IActionResult> AddJob(AddJobRequest request)
    {
        var talentId = User.Identity?.Name;
        var command = _mapper.Map<AddJobCommand>((request, talentId));

        var result = await _mediator.Send(command);
        
        return result.Match<IActionResult>(
            job => Ok(_mapper.Map<AddJobResponse>(job)),
            Problem
        );

    }
    
    [HttpGet("{jobId}")]
    public async Task<IActionResult> GetJobDetails([FromRoute]GetJobRequest request)
    {
        var query = _mapper.Map<GetJobDetailsQuery>(request);

        var result = await _mediator.Send(query);

        return result.Match<IActionResult>(
            job => Ok(_mapper.Map<GetJobResponse>(job)),
            Problem
        );
    }
    
    [HttpGet("talent/{talentId}")]
    public async Task<IActionResult> GetJobsWithTalentId([FromQuery]GetJobsWithTalentIdRequest request, [FromRoute] string talentId)
    {
        var query = _mapper.Map<GetJobsWithTalentIdQuery>((request, talentId));

        var result = await _mediator.Send(query);

        return result.Match<IActionResult>(
            jobs => Ok(_mapper.Map<GetJobsResponse>(jobs)),
            Problem
        );
    }
}