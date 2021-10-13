using System.Threading.Tasks.Dataflow;
using System.IO;
using System.Reflection.Metadata;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Linq;
using System;
using Web.Models;
using Web.Interfaces;
using Cfg.Configuration;

namespace Web.Controllers
{
    [ApiController]
    [Route("configuration")]
    public class ConfigurationController : ControllerBase
    {
        private readonly IConfigurationService _configurationService;

        public ConfigurationController(IConfigurationService configurationService)
        {
            _configurationService = configurationService;
        }

        [HttpPost("compare")]
        public async Task<IActionResult> Compare(IFormFile sourceFile, IFormFile targetFile, [FromQuery] CompareQuery query)
        {
            var visibility = new List<ComparisonStatus>();
            Action<ComparisonStatus, bool?> mapVisibility = (status, value) => {
                if (value == true) visibility.Add(status);
            };
            mapVisibility(ComparisonStatus.Unchanged, query.Unchanged);
            mapVisibility(ComparisonStatus.Modified, query.Modified);
            mapVisibility(ComparisonStatus.Added, query.Added);
            mapVisibility(ComparisonStatus.Removed, query.Removed);
            visibility = visibility.Count == 0 ? null : visibility;

            try {
                var result = _configurationService.CompareAndFilter(sourceFile, targetFile, new FilterOptions() {
                    IdStartsWith = query.IdStartsWith,
                    Visibility = visibility,
                });

                return Ok(await Task.FromResult(result));
            } catch {
                return Problem(
                    statusCode: StatusCodes.Status415UnsupportedMediaType,
                    title: "Both provided files must be 'configuration' files");
            }
        }
    }
}
