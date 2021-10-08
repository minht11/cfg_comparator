using System.Collections.Generic;
using Cfg.Configuration;

namespace Cfg.ConfigUI
{
    public interface IOptions
    {
        string SourcePath { get; set; }

        string TargetPath { get; set; }

        List<ComparisonStatus> Visibility { get; set; }

        string IdStartsWith { get; set; }
    }
}
