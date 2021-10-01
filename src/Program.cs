namespace CfgComparator
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleConfigUI.Input.ListenForUserInput((parsedInput) => {
                ConfigUI.Runner.Display(parsedInput, new ConsoleConfigUI.Output());
            });
        }
    }
}
