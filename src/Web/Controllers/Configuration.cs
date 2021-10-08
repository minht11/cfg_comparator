using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Linq;
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

            var comparedParams = Analyzer.Compare(sourceRecord, targetRecord);
            var filteredParams = FilterByQuery(comparedParams, query);

            return Ok(await Task.FromResult(filteredParams));
        }

        private static List<ComparedParameter> FilterByQuery(List<ComparedParameter> parameters, CompareQuery query)
        {
            var idStarts = query.IdStartsWith;
            var shouldNotFilterById = string.IsNullOrEmpty(idStarts);
            var shouldNotFilterByStatus = (
                query.Unchanged == null
                && query.Modified == null
                && query.Added == null
                && query.Removed == null
            );

            return parameters.Where((param) => {
                var visibleStatus = true;
                if (!shouldNotFilterByStatus)
                {
                    visibleStatus = param.Status switch
                    {
                        ComparisonStatus.Unchanged => query.Unchanged,
                        ComparisonStatus.Modified => query.Modified,
                        ComparisonStatus.Added => query.Added,
                        ComparisonStatus.Removed => query.Removed,
                        _ => false,
                    } ?? false;
                }
      
                return visibleStatus && (shouldNotFilterById || param.ID.StartsWith(idStarts!));

            }).ToList();
        }
    }
}
