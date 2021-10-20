using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Http;
using Web.Models;
using Cfg.ConfigUI;
using Cfg.Configuration;
using Cfg.Interfaces;

namespace Web.Interfaces
{
    public interface IConfigurationService
    {
        bool Upload(IFormFile sourceFile, IFormFile targetFile);

        IResult<ComparisonResult> CompareAndFilter(List<string>? filterByStatus, string? idStartsWith);
    }
}