using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Cfg.Config;

namespace Cfg.ConfigCli
{
    public class Comparison
    {
        [Required]
        public ConfigInfo SourceInfo { get; init; } = default!;
    
        public ConfigInfo TargetInfo { get; init; } = default!;
        
        public List<ComparedParameter> Parameters { get; init; } = default!;

        public class ConfigInfo
        {
            public string FileName { get; init; } = default!;

            public List<Parameter> Attributes { get; init; } = default!;

            public static ConfigInfo Create(Record record) => new()
            {
                FileName = record.FileName,
                Attributes = record.Attributes,
            };
        }
    }
}
