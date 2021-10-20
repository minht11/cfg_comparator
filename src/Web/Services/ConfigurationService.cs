using System.IO;
using System.Reflection.Metadata;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Session;
using System.Threading.Tasks;
using System.Linq;
using System;
using Web.Models;
using Web.Interfaces;
using Cfg.ConfigUI;
using Cfg.Configuration;

namespace Web.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly ILogger<ConfigurationService> _logger;

        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly Web.Models.Reader _reader;

        private readonly Writer _writer;

        private readonly Runner _runner;

        public ConfigurationService(ILogger<ConfigurationService> logger, IHttpContextAccessor httpContext)
        {
            _logger = logger;
            _httpContextAccessor = httpContext;
            _reader = new();
            _writer = new();
            _runner =  new(_reader, _writer);
        }

        private string CreateAndGetTempFilePath(IFormFile? formFile)
        {
            if (formFile == null)
            {
                return "";
            }

            var filePath = Path.ChangeExtension(Path.GetTempFileName(), Path.GetExtension(formFile.FileName));

            using (var stream = System.IO.File.Create(filePath))
            formFile.CopyTo(stream);

            return filePath;
        }
        
        public bool Upload(IFormFile sourceFile, IFormFile targetFile)
        {
            var session = _httpContextAccessor.HttpContext?.Session;
            if (session == null || sourceFile == null || targetFile == null)
            {
                return false;
            }

            try {
                var sourcePath = CreateAndGetTempFilePath(sourceFile);
                var targetPath = CreateAndGetTempFilePath(sourceFile);

                session.SetString("sourcePath", sourcePath);
                session.SetString("targetPath", sourcePath);

                return true;
            } catch
            {
                return false;
            }
        }

        public Cfg.Interfaces.IResult<ComparisonResult> CompareAndFilter(List<string>? filterByStatus, string? idStartsWith)
        {
            var session = _httpContextAccessor.HttpContext?.Session;
            if (session == null)
            {
                throw new Exception("This service can only be used inside active controller");
            }

            var sourcePath = session.GetString("sourcePath");
            var targetPath = session.GetString("targetPath");

            _reader.AppendMessage(new() {
                SourcePath = sourcePath,
                TargetPath = targetPath,
            });
            _runner.Start();
            return _writer.GetResult();
        }
    }
}
