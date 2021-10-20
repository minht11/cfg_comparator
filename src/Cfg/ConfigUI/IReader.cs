namespace Cfg.ConfigUI
{
    public interface IReader
    {
        RunnerStates Read(out string value);
    }
}
