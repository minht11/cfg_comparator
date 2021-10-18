using System;
using Cfg.Configuration;
using Cfg.ConfigUI;

namespace Cfg.ConsoleConfigUI
{
    public class Reader : BaseUI, IReader
    {
        public IOptions Read()
        {
            DisplaySeparator();
            Display("Input source and target file locations");
            Display("Options:");
            Display($"{UserInput.Constants.Unchanged} : show unchanged");
            Display($"{UserInput.Constants.Added} : show added");
            Display($"{UserInput.Constants.Removed} : show removed");
            Display($"{UserInput.Constants.Modified} : show modified");
            Display($"{UserInput.Constants.Starts}* : keys should start with *");
            DisplaySeparator();
            Display($"Type '{UserInput.Constants.Exit}' to finish");
            DisplaySeparator();

            var rawInput = Console.ReadLine() ?? "";
            var parsedInput = UserInput.Parser.Parse(rawInput);

            return parsedInput;
        }
    }
}
