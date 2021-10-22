using System;
using Cfg.Configuration;
using Cfg.ConfigUI;

namespace Cfg.ConsoleConfigUI
{
    public class Reader : BaseUI, IReader
    {
        public (RunnerAction, string?) Read()
        {
            const string Exit = "exit";
            DisplaySeparator();
            Display("Input source and target file locations");
            Display("Options:");
            Display($"{ConfigUI.UserInput.Constants.FilterByStatus}: show only specific status paramaters:");
            Display($"  {ConfigUI.UserInput.Constants.Unchanged} : unchanged");
            Display($"  {ConfigUI.UserInput.Constants.Added} : added");
            Display($"  {ConfigUI.UserInput.Constants.Removed} : removed");
            Display($"  {ConfigUI.UserInput.Constants.Modified} : modified");
    
            Display($"{ConfigUI.UserInput.Constants.Starts}* : keys should start with *");
            DisplaySeparator();
            Display($"Type '{Exit}' to finish");
            DisplaySeparator();

            var input = Console.ReadLine() ?? "";

            if (input == Exit)
            {
                return (RunnerAction.Exit, null);
            }

            return (RunnerAction.Compare, input);
        }
    }
}
