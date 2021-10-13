using System.Collections.Generic;
using Cfg.Configuration;

namespace Web.Interfaces
{
    public interface IFilterOptions
    {
        List<ComparisonStatus>? Visibility { get; set; }

        string? IdStartsWith { get; set; }
    }
}
