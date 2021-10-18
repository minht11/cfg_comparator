using Microsoft.AspNetCore.Http;
using Web.Models;

namespace Web.Interfaces
{
    public interface IConfigurationService
    {
        ComparisonResult CompareAndFilter(IFormFile source, IFormFile target, IFilterOptions options);
    }
}