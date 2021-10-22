using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Web.Interfaces;

namespace Web.Controllers
{
    [ApiController]
    [Route("configuration")]
    public class ConfigController : ControllerBase
    {
        private readonly IConfigService _configurationService;

        public ConfigController(IConfigService configurationService)
        {
            _configurationService = configurationService;
        }

        [HttpPost("upload")]
        public IActionResult Upload(IFormFile sourceFile, IFormFile targetFile)
        {
            var result = _configurationService.Upload(sourceFile, targetFile);

            if (result.Data)
            {
                return Ok(true);
            }
    
            return Problem(result.Message);
        }

        [HttpGet("compare")]
        public IActionResult Compare([FromQuery] List<string>? status, [FromQuery] string? idStartsWith)
        {
            var result = _configurationService.CompareAndFilter(status, idStartsWith);

            if (result.IsSuccess())
            {
                return Ok(result.Data);
            }

            return Problem(title: result.Message);
        }
    }
}
