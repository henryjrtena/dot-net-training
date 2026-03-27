using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace TodoList.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoSyncController : ControllerBase
{
    [EnableRateLimiting("fixed")]
    [HttpGet]
    public IActionResult GetSyncStatus()
    {
        return Ok(new { CanSync = true, TimestampUtc = DateTime.UtcNow });
    }
}
