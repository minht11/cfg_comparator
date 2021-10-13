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

namespace Web.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly ILogger<ConfigurationService> _logger;

        public ConfigurationService(ILogger<ConfigurationService> logger)
        {
            _logger = logger;
        }

        public ComparisonResult CompareAndFilter(IFormFile sourceFile, IFormFile targetFile, IFilterOptions options)
        {
            var sourceFileName = sourceFile?.FileName ?? "";
            var targetFileName = targetFile?.FileName ?? "";

            var sourceRecord = Reader.Read(sourceFile!.OpenReadStream(), sourceFileName);
            var targetRecord = Reader.Read(targetFile!.OpenReadStream(), targetFileName);
            
            var comparedParams = Analyzer.Compare(sourceRecord, targetRecord);
            var filteredParams = Filter(comparedParams, options);

            return new ComparisonResult() {
                SourceInfo = new() {
                    FileName = sourceRecord.FileName,
                    Info = sourceRecord.Info,
                },
                TargetInfo = new() {
                    FileName = targetRecord.FileName,
                    Info = targetRecord.Info,
                },
                Parameters = filteredParams,
            };
        }

        private static List<ComparedParameter> Filter(List<ComparedParameter> parameters, IFilterOptions options)
        {
            var visibility = options.Visibility;
            var idStartsWith = options.IdStartsWith;

            var shouldNotFilterById = string.IsNullOrEmpty(idStartsWith);

            return parameters.Where((param) => {
                var statusMatch = visibility == null || visibility.Contains(param.Status);
                var idMatch = shouldNotFilterById || param.ID.StartsWith(idStartsWith!);

                return statusMatch && idMatch;
            }).ToList();
        }
    }
}
