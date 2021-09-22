using System.ComponentModel;
using System.Collections.Generic;
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

        private static ConsoleColor GetStatusColor(ComparisonStatus status) => status switch
        {
            ComparisonStatus.Unchanged => ConsoleColor.Gray,
            ComparisonStatus.Modified => ConsoleColor.Yellow,
            ComparisonStatus.Added => ConsoleColor.Green,
            ComparisonStatus.Removed => ConsoleColor.Red,
            _ => throw new InvalidEnumArgumentException("Wrong argument"),
        };

        private static bool ShouldShowChangedValue(ComparisonStatus status) =>
            status == ComparisonStatus.Modified;

        private static Dictionary<ComparisonStatus, List<ComparedParameter>> GroupAndFilter(List<ComparedParameter> parameters, string idStarts)
        {
            var groupedParams = new Dictionary<ComparisonStatus, List<ComparedParameter>>();            
            foreach (ComparisonStatus status in Enum.GetValues(typeof(ComparisonStatus)))
            {
                groupedParams.Add(status, new List<ComparedParameter>());
            }

            foreach (var item in parameters)
            {
                if (string.IsNullOrEmpty(idStarts) || item.ID.StartsWith(idStarts))
                {
                    groupedParams[item.Status].Add(item);
                }
            }

            return groupedParams;
        }

        private static void DisplayAnalysisItem(List<ComparedParameter> parameters, ComparisonStatus status)
        {
            DisplaySeparator();
            Console.WriteLine(status.ToString());

            Console.ForegroundColor = GetStatusColor(status);
            foreach (var item in parameters)
            {
                var changedValue = ShouldShowChangedValue(status) ? $" -> {item.ChangedValue}" : "";
                Console.WriteLine($"ID: {item.ID}; Value: {item.Value}{changedValue}");
            }
            Console.ResetColor();
        }

        /// <summary>
        /// Shows summary of all changes and depending on the options list of individual changes.
        /// </summary>
        /// <param name="showUnchanged">Show list of unchanged items</param>
        /// <param name="showModified">Show list of modified items</param>
        /// <param name="showAdded">Show list of added items</param>
        /// <param name="showRemoved">Show list of removed items</param>
        /// <param name="keyStarts">Show ids which start with this value or leave empty to show everything</param>
        public static void DisplayAnalysis(List<ComparedParameter> parameters, List<ComparisonStatus> visible, string keyStarts = "")
        {
            var groupedParams = GroupAndFilter(parameters, keyStarts);

            var unchangedCount = groupedParams[ComparisonStatus.Unchanged].Count;
            var modifiedCount = groupedParams[ComparisonStatus.Modified].Count;
            var addedCount = groupedParams[ComparisonStatus.Added].Count;
            var removedCount = groupedParams[ComparisonStatus.Removed].Count;

            DisplaySeparator();
            Console.WriteLine($"U: {unchangedCount} M: {modifiedCount} R: {addedCount} A: {removedCount}");
            foreach (var status in visible)
            {
                DisplayAnalysisItem(groupedParams[status], status);
            }
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

            foreach (var item in record.Info)
            {
                Console.WriteLine($"{item.ID}: {item.Value}");
            }
        }
    }
}
