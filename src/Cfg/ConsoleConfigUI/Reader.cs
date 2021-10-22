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
            Display($"{ConfigUI.Input.Constants.FilterByStatus}: show only specific status paramaters:");
            Display($"  {ConfigUI.Input.Constants.Unchanged} : unchanged");
            Display($"  {ConfigUI.Input.Constants.Added} : added");
            Display($"  {ConfigUI.Input.Constants.Removed} : removed");
            Display($"  {ConfigUI.Input.Constants.Modified} : modified");
    
            Display($"{ConfigUI.Input.Constants.Starts}* : keys should start with *");
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
