using System.Collections.Generic;
using System.Linq;
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

            string line = Console.ReadLine() ?? "";

            var input = new List<string>(line.Split(' '));
            var sourcePath = input.ElementAtOrDefault(0) ?? "";
            var targetPath = input.ElementAtOrDefault(1) ?? "";

            if (sourcePath == "" || targetPath == "")
            {
                Console.WriteLine("Source and target paths cannot be empty");
                return;
            }
            input.RemoveRange(0, 2);

            bool showUnchanged = input.Contains(unchanged);
            bool showAdded = input.Contains(added);
            bool showRemoved = input.Contains(removed);
            bool showModified = input.Contains(modified);
            string startsValue = input.Find((value) => value.StartsWith(starts))?.Split('=')?[1] ?? "";

            var source = Configuration.Reader.Read(sourcePath);
            var target = Configuration.Reader.Read(targetPath);
            var analysis = Configuration.Analysis.Analyse(source, target);
            
            ResultsUI.DisplayInfo(source, "Source");
            ResultsUI.DisplayInfo(target, "Target");

            ResultsUI.DisplayAnalysis(analysis, showUnchanged, showModified, showAdded, showRemoved, startsValue);
        }
    }
}
