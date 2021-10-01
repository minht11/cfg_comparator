using System.Collections.Generic;

namespace CfgComparator.ConsoleConfigUI.UserInput
{
    public class Result : ConfigUI.IOptions
    {
        public string SourcePath { get; set; } = "";
        public string TargetPath { get; set; } = "";
        public List<Configuration.ComparisonStatus> Visibility { get; set; } = new();
        public string IdStartsWith { get; set; } = "";   
    }
}
