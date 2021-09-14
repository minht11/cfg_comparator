using System.Collections.Generic;
using System;

namespace CfgComparator
{
    class ResultsUI
    {
        static private void DisplaySeparator()
        {
            Console.WriteLine("---------------------------");
        }

        static private void DisplayAnalysisSection(Dictionary<int, string> data, string title, ConsoleColor color, bool show, string keyStarts)
        {
            DisplayAnalysisSection(data, title, color, show, keyStarts, (value) => value);
        }

        static private void DisplayAnalysisSection<T>(Dictionary<int, T> data, string title, ConsoleColor color, bool show, string keyStarts, Func<T, string> formatValue)
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

        static public void DisplayAnalysis(CfgAnalysis.Result analysis, bool showUnchanged, bool showModified, bool showAdded, bool showRemoved, string keyStarts = "")
        {
            DisplaySeparator();
            Console.WriteLine($"U: {analysis.Unchanged.Count} M: {analysis.Modified.Count} R: {analysis.Removed.Count} A: {analysis.Added.Count}");
            
            DisplayAnalysisSection(analysis.Unchanged, "Unchanged:", ConsoleColor.Gray, showUnchanged, keyStarts);
            DisplayAnalysisSection(analysis.Added, "Added:", ConsoleColor.Green, showAdded, keyStarts);
            DisplayAnalysisSection(analysis.Removed, "Removed:", ConsoleColor.Red, showRemoved, keyStarts);
            DisplayAnalysisSection(analysis.Modified, "Modified:", ConsoleColor.Yellow, showModified, keyStarts, (value) => $"{value.Item1} -> {value.Item2}");
        }

        static public void DisplayInfo(CfgRecord record, string name)
        {
            DisplaySeparator();
            Console.WriteLine($"{name} configuration:");
            foreach (var item in record.Info) {
                Console.WriteLine($"{item.Key}: {item.Value}");
            }
        }
    }
}
