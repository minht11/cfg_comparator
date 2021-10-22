using System.Collections.Generic;
using Cfg.Config;

namespace Cfg.ConfigCli
{
    public interface IFilterOptions
    {
        List<ComparisonStatus>? Visibility { get; set; }

        string? IdStartsWith { get; set; }
    }
}
