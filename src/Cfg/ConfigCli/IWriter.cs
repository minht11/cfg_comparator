using Cfg.Interfaces;

namespace Cfg.ConfigCli
{
    public interface IWriter
    {
        void Write(IResult<ComparisonResult> result);
    }
}
