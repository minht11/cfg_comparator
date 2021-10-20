using Cfg.Interfaces;

namespace Cfg.ConfigUI
{
    public interface IWriter
    {
        void Write(IResult<ComparisonResult> result);
    }
}
