using System;

namespace CfgComparator.UI
{
    public class Input : BaseUI
    {
        static private void DisplayInputInstructions()
        {
            DisplaySeparator();
            Console.WriteLine("Input source and target file locations");
            Console.WriteLine("Options:");
            Console.WriteLine($"{UserInput.Constants.Unchanged} : show unchanged");
            Console.WriteLine($"{UserInput.Constants.Added} : show added");
            Console.WriteLine($"{UserInput.Constants.Removed} : show removed");
            Console.WriteLine($"{UserInput.Constants.Modified} : show modified");
            Console.WriteLine($"{UserInput.Constants.Starts}* : keys should start with *");
            DisplaySeparator();
            Console.WriteLine($"Type '{UserInput.Constants.Exit}' to finish");
            DisplaySeparator();
        }

        static public void ListenForUserInput(Action<UserInput.Result> onInput) {
            while (true)
            {
                DisplayInputInstructions();
                var input = Console.ReadLine() ?? "";

                if (input == UserInput.Constants.Exit)
                {
                    break;
                }

                var options = UserInput.Parser.Parse(input);
                onInput(options);
            }
        }
    }
}
