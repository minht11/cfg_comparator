using System.Collections.Generic;
using Cfg.Configuration;
using Web.Interfaces;

namespace Web.Models
{
    public class FilterOptions : IFilterOptions
    {
        public List<ComparisonStatus>? Visibility { get; set; }

        public string? IdStartsWith { get; set; }
    }
}
