using Cfg.ConfigCliFrontend;

namespace Cfg
{
    class Program
    {
        static void Main(string[] args)
        {
            var runner = new ConfigCli.Runner(new Reader(), new Writer());

            runner.Start();
        }
    }
}
