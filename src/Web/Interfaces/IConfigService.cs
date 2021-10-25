using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Cfg.ConfigCli;

namespace Web.Interfaces
{
    public interface IConfigService
    {
        void Upload(IFormFile sourceFile, IFormFile targetFile);

        ComparisonResult CompareAndFilter(List<string>? filterByStatus, string? idStartsWith);
    }
}