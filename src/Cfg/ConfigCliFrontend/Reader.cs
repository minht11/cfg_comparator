using System;
using Cfg.ConfigCli;

namespace Cfg.ConfigCliFrontend
{
    using Constants = ConfigCli.Input.Constants;
    public class Reader : BaseUI, IReader
    {
        public (RunnerAction, string?) Read()
        {
            const string Exit = "exit";
            DisplaySeparator();
            Display("Input source and target file locations");
            Display("Options:");
            Display($"{Constants.StatusArg}*: show only specific status paramaters, list separated by commas:");
            Display($"  {Constants.Unchanged} : unchanged");
            Display($"  {Constants.Added} : added");
            Display($"  {Constants.Removed} : removed");
            Display($"  {Constants.Modified} : modified");
    
            Display($"{Constants.StartsArg}* : ids should start with");
            DisplaySeparator();
            Display($"Type '{Exit}' to finish");
            DisplaySeparator();

            var input = Console.ReadLine();

            if (input == Exit)
            {
                return (RunnerAction.Exit, null);
            }

            return (RunnerAction.Compare, input);
        }
    }
}
