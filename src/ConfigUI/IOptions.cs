using System.Collections.Generic;
using CfgComparator.Configuration;

namespace CfgComparator.ConfigUI
{
    interface IOptions
    {
        string SourcePath { get; set; }

        string TargetPath { get; set; }

        List<ComparisonStatus> Visibility { get; set; }

        string IdStartsWith { get; set; }
    }
}
