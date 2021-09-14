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
            // string sourcePath = "./test-data/FMB920-default.cfg";
            // string targetPath = "./test-data/FMB920-modified.cfg";
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
            
            DisplayInfo(source, "Source");
            DisplayInfo(target, "Target");

            DisplayAnalysis(analysis, showUnchanged, showModified, showAdded, showRemoved, startsValue);
        }

        static void DisplaySeparator() {
            Console.WriteLine("---------------------------");
        }

        static void DisplayInfo(CfgRecord record, string name)
        {
            DisplaySeparator();
            Console.WriteLine($"{name} configuration:");
            foreach (var item in record.Info) {
                Console.WriteLine($"{item.Key}: {item.Value}");
            }
        }

        static void DisplayAnalysisSection(Dictionary<int, string> data, string title, ConsoleColor color, bool show, string keyStarts)
        {
            DisplayAnalysisSection(data, title, color, show, keyStarts, (value) => value);
        }

        static void DisplayAnalysisSection<T>(Dictionary<int, T> data, string title, ConsoleColor color, bool show, string keyStarts, Func<T, string> formatValue)
        {
            Func<int, bool> showKey = (key) => keyStarts == "" || key.ToString().StartsWith(keyStarts);
            
            if (show) {
                DisplaySeparator();
                Console.WriteLine(title);
                Console.ForegroundColor = color;
                foreach (var item in data)
                {
                    if (showKey(item.Key)) {
                        Console.WriteLine($"ID: {item.Key}; Value: {formatValue(item.Value)}");
                    }
                }
                Console.ResetColor();
            }
        }

        static void DisplayAnalysis(CfgAnalysis.Result analysis, bool showUnchanged, bool showModified, bool showAdded, bool showRemoved, string keyStarts = "")
        {
            DisplaySeparator();
            Console.WriteLine($"U: {analysis.Unchanged.Count} M: {analysis.Modified.Count} R: {analysis.Removed.Count} A: {analysis.Added.Count}");
            
            DisplayAnalysisSection(analysis.Unchanged, "Unchanged:", ConsoleColor.Gray, showUnchanged, keyStarts);
            DisplayAnalysisSection(analysis.Added, "Added:", ConsoleColor.Green, showAdded, keyStarts);
            DisplayAnalysisSection(analysis.Removed, "Removed:", ConsoleColor.Red, showRemoved, keyStarts);
            DisplayAnalysisSection(analysis.Modified, "Modified:", ConsoleColor.Yellow, showModified, keyStarts, (value) => $"{value.Item1} -> {value.Item2}");
        }
    }
}
