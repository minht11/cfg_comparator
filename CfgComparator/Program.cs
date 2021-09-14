using System.Collections.Generic;
using System;

namespace CfgComparator
{
    class Program
    {
        static void Main(string[] args)
        {
            const string unchanged = "-u";
            const string added = "-a";
            const string removed = "-r";
            const string modified = "-m";
            const string starts = "--starts=";

            Console.WriteLine("Input source and target file locations");
            Console.WriteLine("Options:");
            Console.WriteLine($"{unchanged} : show unchanged");
            Console.WriteLine($"{added} : show added");
            Console.WriteLine($"{removed} : show removed");
            Console.WriteLine($"{modified} : show modified");
            Console.WriteLine($"{starts}* : keys should start with *");

            string line = Console.ReadLine();
            if (line == "" || line == null) {
                return;
            }

            var input = new List<string>(line.Split(' '));
            var sourcePath = input?[0];
            var targetPath = input?[1];
            input.RemoveAt(0);
            input.RemoveAt(0);

            if (sourcePath == null || targetPath == null)
            {
                Console.WriteLine("Source and target paths cannot be empty");
                return;
            }

            bool showUnchanged = input.Contains(unchanged);
            bool showAdded = input.Contains(added);
            bool showRemoved = input.Contains(removed);
            bool showModified = input.Contains(modified);
            string startsWithOption = input.Find((value) => value.StartsWith(starts));
            string startsValue = "";
            if (startsWithOption != null)
            {
                startsValue = startsWithOption.Split('=')?[1];
            }

            CfgReader reader = new();
            var source = reader.Read(sourcePath);
            var target = reader.Read(targetPath);
            var analysis = CfgAnalysis.Analyse(source, target);
            
            ResultsUI.DisplayInfo(source, "Source");
            ResultsUI.DisplayInfo(target, "Target");

            ResultsUI.DisplayAnalysis(analysis, showUnchanged, showModified, showAdded, showRemoved, startsValue);
        }
    }
}
