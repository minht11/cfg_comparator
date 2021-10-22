namespace Cfg.ConfigUI
{
    public interface IReader
    {
        (Actions nextAction, string? value) Read();
    }
}
