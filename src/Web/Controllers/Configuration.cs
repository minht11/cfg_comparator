using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System;
using Web.Models;

namespace Web.Controllers
{
    [ApiController]
    [Route("configuration")]
    public class Configuration : ControllerBase
    {
        private readonly ILogger<Configuration> _logger;

        public Configuration(ILogger<Configuration> logger)
        {
            _logger = logger;
        }

        [HttpPost("compare")]
        public async Task<IActionResult> Compare(IFormFile sourceFile, IFormFile targetFile, [FromQuery] CompareQuery query)
        {
            // Console.WriteLine(query);
            await Task.Delay(100);
            return Ok("Umm");
        }
    }
}
