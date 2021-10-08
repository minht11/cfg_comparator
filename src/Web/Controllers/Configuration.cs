using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System;
using Web.Models;
using Cfg.Configuration;

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
            var sourceRecord = Reader.Read(sourceFile.OpenReadStream(), sourceFile.FileName);
            var targetRecord = Reader.Read(targetFile.OpenReadStream(), targetFile.FileName);

            var analysis = Analyzer.Compare(sourceRecord, targetRecord);

            return Ok(await Task.FromResult(analysis));
        }
    }
}
