using System;

namespace CfgComparator.UI
{
    public class Input
    {
        static private void DisplayInputInstructions()
        {
            Console.WriteLine("Input source and target file locations");
            Console.WriteLine("Options:");
            Console.WriteLine($"{UserInput.Constants.Unchanged} : show unchanged");
            Console.WriteLine($"{UserInput.Constants.Added} : show added");
            Console.WriteLine($"{UserInput.Constants.Removed} : show removed");
            Console.WriteLine($"{UserInput.Constants.Modified} : show modified");
            Console.WriteLine($"{UserInput.Constants.Starts}* : keys should start with *");
        }

        static public UserInput.Result GetParsedUserInput()
        {
            DisplayInputInstructions();

            string line = Console.ReadLine() ?? "";
            return UserInput.Parser.Parse(line);
        }
    }
}
