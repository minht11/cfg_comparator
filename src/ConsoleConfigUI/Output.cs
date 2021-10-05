using System;
using System.Collections.Generic;
using CfgComparator.Configuration;

namespace CfgComparator.ConsoleConfigUI
{
    using GroupedParameters = Dictionary<ComparisonStatus, List<ComparedParameter>>;

    public class Output : BaseUI, ConfigUI.IDisplayImpl
    {
        private static GroupedParameters GroupByStatus(List<ComparedParameter> parameters)
        {
            var groupedParams = new GroupedParameters();            
            foreach (ComparisonStatus status in Enum.GetValues(typeof(ComparisonStatus)))
            {
                groupedParams.Add(status, new List<ComparedParameter>());
            }

            foreach (var item in parameters)
            {
                groupedParams[item.Status].Add(item);
            }

            return groupedParams;
        }

        private static ConsoleColor GetStatusColor(ComparisonStatus status) => status switch
        {
            ComparisonStatus.Unchanged => ConsoleColor.Gray,
            ComparisonStatus.Modified => ConsoleColor.Yellow,
            ComparisonStatus.Added => ConsoleColor.Green,
            ComparisonStatus.Removed => ConsoleColor.Red,
            _ => throw new ArgumentException("Provided enum value is not valid"),
        };

        private static bool ShouldShowChangedValue(ComparisonStatus status) =>
            status == ComparisonStatus.Modified;

        static private void DisplaySummary(GroupedParameters groupedParams)
        {
            var unchangedCount = groupedParams[ComparisonStatus.Unchanged].Count;
            var modifiedCount = groupedParams[ComparisonStatus.Modified].Count;
            var addedCount = groupedParams[ComparisonStatus.Added].Count;
            var removedCount = groupedParams[ComparisonStatus.Removed].Count;

            Console.WriteLine($"U: {unchangedCount} M: {modifiedCount} R: {addedCount} A: {removedCount}");
        }

        private static void DisplayComparisonSection(List<ComparedParameter> parameters, ComparisonStatus status)
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

        public void DisplayComparisons(List<ComparedParameter> parameters, List<ComparisonStatus> visibility)
        {
            var groupedParams = GroupByStatus(parameters);

            DisplaySeparator();
            DisplaySummary(groupedParams);
            foreach (var status in visibility)
            {
                DisplayComparisonSection(groupedParams[status], status);
            }
        }

        private static void DisplayRecordInfo(Record record, string name)
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

        public void DisplayRecordsInfo(Record source, Record target)
        {
            DisplayRecordInfo(source, "Source");
            DisplayRecordInfo(target, "Target");
        }

        public void DisplayError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
