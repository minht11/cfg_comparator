using System;
namespace CfgComparator
{
    class Program
    {
        static void Main(string[] args)
        {
            UI.Input.ListenForUserInput(OnUserInputHandler);
        }

        private static void OnUserInputHandler(UserInput.Result options)
        {
            try
            {
                var source = Configuration.Reader.Read(options.SourcePath);
                var target = Configuration.Reader.Read(options.TargetPath);
                var analysis = Configuration.Analyzer.Compare(source, target);
                
                UI.Output.DisplayInfo(source, "Source");
                UI.Output.DisplayInfo(target, "Target");

                UI.Output.DisplayAnalysis(analysis, options.Visible, options.KeyStartsWith);
            }
            catch (Exception err)
            {
                HandleUserInputException(err);
            }
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
