using System;
using System.IO;
namespace CfgComparator
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = UI.Input.GetParsedUserInput();

            if (ValidateFilePath(options.SourcePath) && ValidateFilePath(options.TargetPath))
            {
                var source = Configuration.Reader.Read(options.SourcePath);
                var target = Configuration.Reader.Read(options.TargetPath);
                var analysis = Configuration.Analyzer.Compare(source, target);
                
                UI.Output.DisplayInfo(source, "Source");
                UI.Output.DisplayInfo(target, "Target");

                UI.Output.DisplayAnalysis(analysis, options.Visible, options.KeyStartsWith);
            }
            else
            {
                Console.WriteLine("Source or/and target paths are empty or do not exits");
            }
        }

        static private bool ValidateFilePath(string? path)
        {
            return !string.IsNullOrEmpty(path) && File.Exists(path);
        }
    }
}
