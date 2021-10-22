using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Cfg.ConfigCli;
using Cfg.Interfaces;

namespace Web.Interfaces
{
    public interface IConfigService
    {
        IResult<bool> Upload(IFormFile sourceFile, IFormFile targetFile);

        IResult<ComparisonResult> CompareAndFilter(List<string>? filterByStatus, string? idStartsWith);
    }
}