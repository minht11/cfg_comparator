using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using Cfg.Configuration;

namespace Cfg.ConfigUI
{
    public class ComparisonResult
    {
        public ConfigInfo SourceInfo { get; set; } = new();
    
        public ConfigInfo TargetInfo { get; set; } = new();
        
        public List<ComparedParameter> Parameters { get; set; } = new();

        public class ConfigInfo
        {
            public string FileName { get; set; } = "";

            public List<Parameter> Attributes { get; set; } = new();
        }
    }
}
