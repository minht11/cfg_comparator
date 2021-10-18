using Cfg.ConsoleConfigUI;

namespace Cfg
{
    class Program
    {
        static void Main(string[] args)
        {
            var runner = new ConfigUI.Runner(new Reader(), new Writer());
            runner.Start();
        }
    }
}
