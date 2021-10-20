using System.IO;

namespace Cfg.ConfigUI
{
    public interface IPathOptions
    {
        string SourcePath { get; set; }

        string TargetPath { get; set; }
    }
}
