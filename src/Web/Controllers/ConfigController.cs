using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Web.Interfaces;
using Web.Extensions.IFormFileExtensions;

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
            if (!sourceFile.IsConfigurationFile() || !targetFile.IsConfigurationFile())
            {
                return Problem("Both provided files must be valid '.cfg' files.");
            }

            try
            {
                var sourcePath = sourceFile.CreateAndGetTempFilePath();
                var targetPath = targetFile.CreateAndGetTempFilePath();

                var session = HttpContext.Session;

                session.SetString("sourcePath", sourcePath);
                session.SetString("targetPath", targetPath);
                session.SetString("sourceRealFileName", sourceFile.FileName);
                session.SetString("targetRealFileName", targetFile.FileName);

                return Ok(true);
            }
            catch
            {
                return Problem("Unknown error occured while trying to upload your files");
            }
        }

        [HttpGet("compare")]
        public IActionResult Compare([FromQuery] List<string>? status, [FromQuery] string? idStartsWith)
        {
            var session = HttpContext.Session;

            var targetPath = session.GetString("sourcePath");
            var sourcePath = session.GetString("targetPath");

            var sourceRealFileName = session.GetString("targetRealFileName");
            var targetRealFileName = session.GetString("sourceRealFileName");

            if (string.IsNullOrEmpty(sourcePath) || string.IsNullOrEmpty(targetPath))
            {
                return Problem("In order to run comparison, source and target files must be uploaded first.");
            }

            var result = _configurationService.CompareAndFilter(new() {
                SourcePath = sourcePath,
                TargetPath = targetPath,
                FilterByStatus = status,
                IdStartsWith = idStartsWith,
            });

            if (result.IsSuccess())
            {
                var data = result.Data;
                return Ok(new Cfg.ConfigCli.Comparison() {
                    Parameters = data.Parameters,
                    SourceInfo = new() {
                        FileName = sourceRealFileName,
                        Attributes = data.SourceInfo.Attributes,
                    },
                    TargetInfo = new() {
                        FileName = targetRealFileName,
                        Attributes = data.TargetInfo.Attributes,
                    },
                });
            }

            return Problem(title: result.Message);
        }
    }
}
