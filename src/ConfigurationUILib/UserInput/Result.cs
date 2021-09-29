using System.Collections.Generic;

namespace CfgComparator.ConfigurationUILib.UserInput
{
    public class Result : IAnalysisOptions
    {
        public string SourcePath { get; set; } = "";
        public string TargetPath { get; set; } = "";
        public List<Configuration.ComparisonStatus> Visible { get; set; } = new();
        public string KeyStartsWith { get; set; } = "";   
    }
}
