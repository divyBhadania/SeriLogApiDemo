using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Serilog.Events;

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
        public IActionResult Information()
        {
            _logger.LogInformation("Information logged");
            return Ok();
        }

        [HttpGet("Warning")]
        public IActionResult Warning()
        {
            _logger.LogWarning("Warning logged");
            return Ok();
        }
        
        [HttpGet("Debug")]
        public IActionResult Debug()
        {
            _logger.LogDebug("Debug logged");
            return Ok();
        }

        [HttpGet("Error")]
        public IActionResult Error()
        {
            _logger.LogError("Error logged");
            return Ok();
        }

        [HttpGet("Fatal")]
        public IActionResult Fatal()
        {
            _logger.LogCritical("Fatal logged");
            return Ok();
        }

        [HttpGet("GenerateException")]
        public IActionResult GenerateError()
        {
            throw new NotImplementedException();
            return Ok();
        }
    }
}
