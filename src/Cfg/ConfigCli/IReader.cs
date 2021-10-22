namespace Cfg.ConfigCli
{
    public interface IReader
    {
        (RunnerAction nextAction, string? value) Read();
    }
}
