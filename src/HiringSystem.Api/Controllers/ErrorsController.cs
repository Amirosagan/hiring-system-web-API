using Microsoft.AspNetCore.Mvc;

namespace HiringSystem.Api.Controllers;

[ApiController]
public class ErrorsController : ControllerBase
{
    [Route("/error")]
    public IActionResult Error()
    {
        return Problem();
    }
}