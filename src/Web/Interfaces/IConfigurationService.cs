using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Web.Models;
using Cfg.Configuration;

namespace Web.Interfaces
{
    public interface IConfigurationService
    {
        ComparisonResult CompareAndFilter(IFormFile source, IFormFile target, IFilterOptions options);
    }
}