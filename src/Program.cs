namespace CfgComparator
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleConfigUI.Input.ListenForUserInput((parsedInput) => {
                ConfigUI.Runner.Start(parsedInput, new ConsoleConfigUI.Output());
            });
        }
    }
}
