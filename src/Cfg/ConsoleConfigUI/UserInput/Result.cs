using System.Collections.Generic;

namespace Cfg.ConsoleConfigUI.UserInput
{
    public class Result : ConfigUI.IOptions
    {
        public string SourcePath { get; set; } = "";
        public string TargetPath { get; set; } = "";
        public List<Configuration.ComparisonStatus>? Visibility { get; set; }
        public string? IdStartsWith { get; set; }
    }
}
