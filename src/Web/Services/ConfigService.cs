using System.IO;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System;
using Web.Models;
using Web.Interfaces;
using Cfg.ConfigCli;

namespace Web.Services
{
    public class ConfigService : IConfigService
    {
        private readonly ILogger<ConfigService> _logger;

        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly Reader _reader;

        private readonly Writer _writer;

        private readonly Runner _runner;

        public ConfigService(ILogger<ConfigService> logger, IHttpContextAccessor httpContext)
        {
            _logger = logger;
            _httpContextAccessor = httpContext;
            _reader = new();
            _writer = new();
            _runner =  new(_reader, _writer);
        }

        private string CreateAndGetTempFilePath(IFormFile formFile)
        {
            var filePath = Path.ChangeExtension(Path.GetTempFileName(), Path.GetExtension(formFile.FileName));

            using (var stream = File.Create(filePath))
            formFile.CopyTo(stream);

            return filePath;
        }

        public static bool ValidateFileType([NotNullWhen(true)] IFormFile? file) =>
            file != null && Path.GetExtension(file.FileName) == ".cfg";

        public void Upload(IFormFile sourceFile, IFormFile targetFile)
        {
            var session = _httpContextAccessor.HttpContext?.Session;
            if (session == null)
            {
                throw new Exception("This service can only be used inside active controller");
            }

            if (!ValidateFileType(sourceFile) || !ValidateFileType(targetFile))
            {
                throw new ArgumentException("Both provided files must be valid '.cfg' files.");
            }

            var sourcePath = CreateAndGetTempFilePath(sourceFile);
            var targetPath = CreateAndGetTempFilePath(targetFile);

            session.SetString("sourcePath", sourcePath);
            session.SetString("targetPath", targetPath);
        }

        public ComparisonResult CompareAndFilter(List<string>? status, string? idStartsWith)
        {
            var session = _httpContextAccessor.HttpContext?.Session;
            if (session == null)
            {
                throw new Exception("This service can only be used inside active controller");
            }

            var sourcePath = session.GetString("sourcePath");
            var targetPath = session.GetString("targetPath");

            if (string.IsNullOrEmpty(sourcePath) || string.IsNullOrEmpty(sourcePath))
            {
                return new ComparisonResult() {
                    Message = "In order to run comparison, source and target files must be uploaded first.",
                };
            }

            _reader.AppendMessage(new() {
                SourcePath = sourcePath,
                TargetPath = targetPath,
                FilterByStatus = status,
                IdStartsWith = idStartsWith,
            });
            _runner.Start();
            return _writer.GetResult();
        }
    }
}
