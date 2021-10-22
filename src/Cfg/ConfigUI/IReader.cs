namespace Cfg.ConfigUI
{
    public interface IReader
    {
        (RunnerAction nextAction, string? value) Read();
    }
}
