using System.Collections.Generic;
using Cfg.Configuration;

namespace Web.Models
{
    public class ComparisonResult
    {
        public ConfigInfo SourceInfo { get; set; } = new();
    
        public ConfigInfo TargetInfo { get; set; } = new();
        
        public List<ComparedParameter> Parameters { get; set; } = new();

        public class ConfigInfo {
            public string FileName { get; set; } = "";

            public List<Parameter> Info { get; set; } = new();
        }
    }
}
