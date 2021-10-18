using System.Collections.Generic;
using Cfg.Configuration;

namespace Cfg.ConfigUI
{
    public interface IFilterOptions
    {
        List<ComparisonStatus>? Visibility { get; set; }

        string? IdStartsWith { get; set; }
    }
}
