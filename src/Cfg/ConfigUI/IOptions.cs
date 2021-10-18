using System.IO;

namespace Cfg.ConfigUI
{
    public interface IOptions : IFilterOptions
    {
        string SourcePath { get; set; }

        string TargetPath { get; set; }
    }
}
