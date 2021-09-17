﻿using System.Collections.Generic;
using System.Linq;
using System;
using CfgComparator.Configuration;

namespace CfgComparator
{
    public class ResultsUI
    {
        static private void DisplaySeparator()
        {
            Console.WriteLine("---------------------------");
        }

        private class DisplayAnalysisItemOptions {
            public string Title { get; set; } = "";
            public ConsoleColor Color { get; set; }
            public string KeyStarts { get; set; } = "";
            public bool Visible { get; set; }
            public bool ShowChangedValue { get; set; } = false;
        }

        private static void DisplayAnalysisItem(List<ComparedParameter> data, DisplayAnalysisItemOptions options)
        {
            var keyStarts = options.KeyStarts;
            Func<string, bool> showKey = (key) => keyStarts == "" || key.ToString().StartsWith(keyStarts);
            
            if (options.Visible) {
                DisplaySeparator();
                Console.WriteLine(options.Title);
                Console.ForegroundColor = options.Color;
                foreach (var item in data)
                {
                    if (showKey(item.ID)) {
                        var changedValue = options.ShowChangedValue ? $" -> {item.ChangedValue}" : "";
                        Console.WriteLine($"ID: {item.ID}; Value: {item.Value}{changedValue}");
                    }
                }
                Console.ResetColor();
            }
        }

        /// <summary>
        /// Shows summary of all changes and depending on the options list of individual changes.
        /// </summary>
        /// <param name="showUnchanged">Show list of unchanged items</param>
        /// <param name="showModified">Show list of modified items</param>
        /// <param name="showAdded">Show list of added items</param>
        /// <param name="showRemoved">Show list of removed items</param>
        /// <param name="keyStarts">Show ids which start with this value or leave empty to show everything</param>
        public static void DisplayAnalysis(List<ComparedParameter> changes, bool showUnchanged, bool showModified, bool showAdded, bool showRemoved, string keyStarts = "")
        {
            var changesDict = changes.GroupBy((p) => p.Status).ToDictionary((c) => c.Key, (c) => c.ToList());
            
            var unchanged = changesDict[ComparisonStatus.Unchanged];
            var modified = changesDict[ComparisonStatus.Modified];
            var removed = changesDict[ComparisonStatus.Removed];
            var added = changesDict[ComparisonStatus.Added];

            DisplaySeparator();
            Console.WriteLine($"U: {unchanged.Count} M: {modified.Count} R: {removed.Count} A: {added.Count}");

            DisplayAnalysisItem(unchanged, new DisplayAnalysisItemOptions {
                Title = "Unchanged",
                Color = ConsoleColor.Gray,
                KeyStarts = keyStarts,
                Visible = showUnchanged,
            });
            DisplayAnalysisItem(added, new DisplayAnalysisItemOptions {
                Title = "Added",
                Color = ConsoleColor.Green,
                KeyStarts = keyStarts,
                Visible = showAdded,
            });
            DisplayAnalysisItem(removed, new DisplayAnalysisItemOptions {
                Title = "Removed",
                Color = ConsoleColor.Red,
                KeyStarts = keyStarts,
                Visible = showRemoved,
            });
            DisplayAnalysisItem(modified, new DisplayAnalysisItemOptions {
                Title = "Modified",
                Color = ConsoleColor.Yellow,
                KeyStarts = keyStarts,
                ShowChangedValue = true,
                Visible = showModified,
            });
        }

        /// <summary>
        /// Prints device information to screen.
        /// </summary>
        /// <param name="record">Record of the device that is displayed</param>
        /// <param name="name">Name used in display title</param>
        public static void DisplayInfo(Record record, string name)
        {
            DisplaySeparator();
            Console.WriteLine($"{name} configuration:");
            Console.WriteLine(record.FileName);
            Console.WriteLine("");

            foreach (var item in record.Info) {
                Console.WriteLine($"{item.ID}: {item.Value}");
            }
        }
    }
}
