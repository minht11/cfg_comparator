using System.Collections.Generic;

namespace CfgComparator.UserInput
{
    public class Result
    {
        public string SourcePath { get; set; } = "";
        public string TargetPath { get; set; } = "";
        public List<Configuration.ComparisonStatus> Visible { get; set; } = new();
        public string KeyStartsWith { get; set; } = "";   
    }
}
