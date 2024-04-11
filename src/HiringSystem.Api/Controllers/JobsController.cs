using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HiringSystem.Api.Controllers;

[ApiController]
[Route("api/jobs")]
public class JobsController : ErrorApiController
{
    [HttpGet]
    [Authorize]
    public IActionResult GetJobs()
    {
        return Ok(new List<object>());
    }
}