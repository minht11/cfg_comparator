using System.Collections.Generic;

namespace Cfg.ConfigUI.Input
{
    public class Result : IFilterOptions
    {
        public string SourcePath { get; set; } = "";
        public string TargetPath { get; set; } = "";
        public List<Configuration.ComparisonStatus>? Visibility { get; set; }
        public string? IdStartsWith { get; set; }
    }
}
