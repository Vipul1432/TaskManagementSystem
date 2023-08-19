using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TaskManagementSystem.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class TasksController : ControllerBase
    {
        [HttpGet(Name = "Index")]
        public IActionResult Index()
        {
            return Ok();
        }
    }
}
