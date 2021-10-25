using System.Collections.Generic;
using Cfg.Config;

namespace Cfg.ConfigCli.Input
{
    public class Result : IFilterOptions
    {
        public string SourcePath { get; set; } = "";
        public string TargetPath { get; set; } = "";
        public List<ComparisonStatus>? Visibility { get; set; }
        public string? IdStartsWith { get; set; }
    }
}
