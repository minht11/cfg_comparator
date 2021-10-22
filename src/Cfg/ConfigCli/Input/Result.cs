using System.Collections.Generic;

namespace Cfg.ConfigCli.Input
{
    public class Result : IFilterOptions
    {
        public string SourcePath { get; set; } = "";
        public string TargetPath { get; set; } = "";
        public List<Config.ComparisonStatus>? Visibility { get; set; }
        public string? IdStartsWith { get; set; }
    }
}
