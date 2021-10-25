using System;
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
            try
            {
                _configurationService.Upload(sourceFile, targetFile);
                return Ok(true);
            }
            catch (Exception err)
            {
                if (err is ArgumentException)
                {
                    return Problem("Both provided files must be valid '.cfg' files.");
                }

                return Problem("Unknown error occured while trying to upload your files.");
            }
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
