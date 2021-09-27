using System.Collections.Generic;
using CfgComparator.Configuration;
using System;

namespace CfgComparator.UI
{
    public class Output : BaseUI
    {
        // private static ConsoleColor GetStatusColor(ComparisonStatus status) => status switch
        // {
        //     ComparisonStatus.Unchanged => ConsoleColor.Gray,
        //     ComparisonStatus.Modified => ConsoleColor.Yellow,
        //     ComparisonStatus.Added => ConsoleColor.Green,
        //     ComparisonStatus.Removed => ConsoleColor.Red,
        //     _ => throw new ArgumentException("Provided enum value is not valid"),
        // };

        // private static bool ShouldShowChangedValue(ComparisonStatus status) =>
        //     status == ComparisonStatus.Modified;

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

        private static void DisplayAnalysisItem(List<ComparedParameter> parameters, ComparisonStatus status, Analysis.AnalysisOutputOptions options)
        {
            DisplaySeparator();
            Console.WriteLine(status.ToString());

            Console.ForegroundColor = options.Color;
            foreach (var item in parameters)
            {
                var changedValue = options.ShowChangedValue ? $" -> {item.ChangedValue}" : "";
                Console.WriteLine($"ID: {item.ID}; Value: {item.Value}{changedValue}");
            }
            Console.ResetColor();
        }

        static private void DisplaySummary(Dictionary<ComparisonStatus, List<ComparedParameter>> groupedParams)
        {
            var unchangedCount = groupedParams[ComparisonStatus.Unchanged].Count;
            var modifiedCount = groupedParams[ComparisonStatus.Modified].Count;
            var addedCount = groupedParams[ComparisonStatus.Added].Count;
            var removedCount = groupedParams[ComparisonStatus.Removed].Count;

            Console.WriteLine($"U: {unchangedCount} M: {modifiedCount} R: {addedCount} A: {removedCount}");
        }

        /// <summary>
        /// Shows summary of all changes and depending on the options list of individual changes.
        /// </summary>
        /// <param name="parameters">Parameters list</param>
        /// <param name="showModified">List of parameter types to display</param>
        /// <param name="keyStarts">Show ids which start with this value or leave empty to show everything</param>
        /// <exception cref="System.ArgumentException">Thrown when provided visible enmum is not valid</exception>
        public static void DisplayAnalysis(List<ComparedParameter> parameters, List<ComparisonStatus> visible, string keyStarts = "")
        {
            var groupedParams = GroupAndFilter(parameters, keyStarts);

            var unchanged = new Analysis.Unchanged();
            var modified = new Analysis.Modified();
            var removed = new Analysis.Removed();
            var added = new Analysis.Added();
            unchanged.SetSuccessor(modified);
            modified.SetSuccessor(removed);
            removed.SetSuccessor(added);

            DisplaySeparator();
            DisplaySummary(groupedParams);
            foreach (var status in visible)
            {
                var options = unchanged.GetOptions(status);

                if (options == null) {
                    throw new ArgumentException($"Status value '{status}' could not be resolved");
                }

                DisplayAnalysisItem(groupedParams[status], status, options);
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
            DisplaySpace();

            foreach (var item in record.Info)
            {
                Console.WriteLine($"{item.ID}: {item.Value}");
            }
        }
    }
}
