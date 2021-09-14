using System.Collections.Generic;
using System;

namespace CfgComparator
{
    class Program
    {
        static void Main(string[] args)
        {
            String sourcePath = "./test-data/FMB920-default.cfg";
            String targetPath = "./test-data/FMB920-modified.cfg";

            CfgReader reader = new();
            var source = reader.Read(sourcePath);
            var target = reader.Read(targetPath);
            var analysis = CfgAnalysis.Analyse(source, target);

            Display(analysis, true, true, true, true, "");
        }

        static void DisplaySection(Dictionary<int, string> data, string title, bool show, string keyStarts) {
            DisplaySection(data, title, show, keyStarts, (value) => value);
        }

        static void DisplaySection<T>(Dictionary<int, T> data, string title, bool show, string keyStarts, Func<T, string> formatValue)
        {
            Func<int, bool> showKey = (key) => keyStarts == "" || key.ToString().StartsWith(keyStarts);
            
            if (show) {
                Console.WriteLine("---------------------------");
                Console.WriteLine(title);
                foreach (var item in data)
                {
                    if (showKey(item.Key)) {
                        Console.WriteLine($"ID: {item.Key}; Value: {formatValue(item.Value)}");
                    }
                }
            }
        }

        static void Display(CfgAnalysis.Result analysis, bool showUnchanged, bool showModified, bool showAdded, bool showRemoved, string keyStarts = "")
        {
            DisplaySection(analysis.Unchanged, "Unchanged:", showUnchanged, keyStarts);
            DisplaySection(analysis.Added, "Added:", showAdded, keyStarts);
            DisplaySection(analysis.Removed, "Removed:", showRemoved, keyStarts);
            DisplaySection(analysis.Modified, "Modified:", showRemoved, keyStarts, (value) => $"{value.Item1} -> {value.Item2}");
        }
    }
}
