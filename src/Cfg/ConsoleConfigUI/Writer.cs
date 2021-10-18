using System.Collections.Generic;
using System;
using Cfg.Configuration;
using Cfg.ConfigUI;

namespace Cfg.ConsoleConfigUI
{
    using GroupedParameters = Dictionary<ComparisonStatus, List<ComparedParameter>>;

    public class Writer : BaseUI, IWriter
    { 
        public void WriteException(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Display(message);
            Console.ResetColor();
        }

        public void Write(List<ComparedParameter> parameters)
        {
            var groupedParams = GroupByStatus(parameters);

            DisplaySeparator();
            DisplaySummary(groupedParams);

            foreach (ComparisonStatus status in Enum.GetValues(typeof(ComparisonStatus)))
            {
                DisplayComparisonSection(groupedParams[status], status);
            }
        }

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

            Func<ComparisonStatus, int> getCount = (status) => groupedParams[status].Count;

            var unchangedCount = getCount(ComparisonStatus.Unchanged);
            var modifiedCount = getCount(ComparisonStatus.Modified);
            var addedCount = getCount(ComparisonStatus.Added);
            var removedCount = getCount(ComparisonStatus.Removed);

            Display($"U: {unchangedCount} M: {modifiedCount} R: {addedCount} A: {removedCount}");
        }

        private static void DisplayComparisonSection(List<ComparedParameter> parameters, ComparisonStatus status)
        {
            DisplaySeparator();
            Display(status.ToString());

            Console.ForegroundColor = GetStatusColor(status);
            foreach (var item in parameters)
            {
                var changedValue = ShouldShowChangedValue(status) ? $" -> {item.ChangedValue}" : "";
                Display($"ID: {item.ID}; Value: {item.Value}{changedValue}");
            }
            Console.ResetColor();
        }

        private static void DisplayRecordInfo(Record record, string name)
        {
            DisplaySeparator();
            Display($"{name} configuration:");
            Display(record.FileName);
            DisplaySpace();

            foreach (var item in record.Info)
            {
                Display($"{item.ID}: {item.Value}");
            }
        }

        public void DisplayRecordsInfo(Record source, Record target)
        {
            DisplayRecordInfo(source, "Source");
            DisplayRecordInfo(target, "Target");
        }
    }
}
