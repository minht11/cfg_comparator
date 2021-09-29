using System;

namespace CfgComparator.ConfigurationUILib
{
    public class ConfigurationUI : ConsoleUILib.BaseUI, ConsoleUILib.ILifecycle
    {
        public void DisplayHeader()
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

        public bool DisplayContent(string input)
        {
            if (input == UserInput.Constants.Exit)
            {
                return true;
            }

            try
            {
                var parsedInput = UserInput.Parser.Parse(input);

                var source = Configuration.Reader.Read(parsedInput.SourcePath);
                var target = Configuration.Reader.Read(parsedInput.TargetPath);
                var analysis = Configuration.Analyzer.Compare(source, target);
                
                Output.DisplayInfo(source, "Source");
                Output.DisplayInfo(target, "Target");

                Output.DisplayAnalysis(analysis, parsedInput);
            }
            catch (Exception err)
            {
                HandleUserInputException(err);
            }

            return false;
        }

        private static void HandleUserInputException(Exception err)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            if (err is Configuration.ReaderPathNotValidException)
            {
                Console.WriteLine(err.Message);
            }
            else
            {
                Console.WriteLine("Unknown error occured while trying to process your files");
            }
            Console.ResetColor();
        }
    }
}
