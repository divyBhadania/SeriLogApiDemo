using Microsoft.AspNetCore.Mvc;

namespace SeriLogApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private readonly ILogger _logger;

        public LogController(ILogger<LogController> logger)
        {
            _logger = logger;
        }

        [HttpGet("Information")]
        public IActionResult InformationLog()
        {
            _logger.LogInformation("information logged");
            return Ok();
        }

        [HttpGet("Warning")]
        public IActionResult Warning()
        {
            _logger.LogWarning("warning logged");
            return Ok();
        }

        [HttpGet("GenerateError")]
        public IActionResult GenerateError()
        {
            throw new NotImplementedException();
            return Ok();
        }
    }
}
