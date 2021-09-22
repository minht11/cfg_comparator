using System.IO;
using System;

namespace CfgComparator.UI
{
    public class Input
    {
        static private void DisplayInputInstructions()
        {
            Console.WriteLine("Input source and target file locations");
            Console.WriteLine("Options:");
            Console.WriteLine($"{UserInput.Constants.unchanged} : show unchanged");
            Console.WriteLine($"{UserInput.Constants.added} : show added");
            Console.WriteLine($"{UserInput.Constants.removed} : show removed");
            Console.WriteLine($"{UserInput.Constants.modified} : show modified");
            Console.WriteLine($"{UserInput.Constants.starts}* : keys should start with *");
        }

        static private bool ValidateFilePath(string? path)
        {
            return !string.IsNullOrEmpty(path) && File.Exists(path);
        }

        static public UserInput.Result GetUserInput()
        {
            DisplayInputInstructions();

            string line = Console.ReadLine() ?? "";

            var options = UserInput.Parser.Parse(line);

            if (ValidateFilePath(options.SourcePath) || ValidateFilePath(options.TargetPath))
            {
                Console.WriteLine("Source or/and target paths are empty or do not exits");
            }

            return options;
        }
    }
}
