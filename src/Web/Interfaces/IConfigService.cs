using Cfg.ConfigCli;
using Web.Models;

namespace Web.Interfaces
{
    public interface IConfigService
    {
        ComparisonResult CompareAndFilter(InputOptions options);
    }
}