using Microsoft.AspNetCore.Mvc;
using TCE_API.Models;
using TCE_API.Repositories;

namespace TCE_API.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        [HttpGet("{token}")]
        public SessionModel Get([FromServices] SessionRepository SessionRepository, string token)
        {
            return SessionRepository.Get(token);
        }

        [HttpGet("{token}")]
        [Route("[action]")]
        public IActionResult EndSession([FromServices] SessionRepository SessionRepository, string token)
        {
            return Ok(SessionRepository.EndSession(token));
        }
    }
}
