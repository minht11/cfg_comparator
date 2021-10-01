using System;
using System.Collections.Generic;
using CfgComparator.Configuration;

namespace CfgComparator.ConfigUI
{
    using GroupedParameters = Dictionary<ComparisonStatus, List<ComparedParameter>>;

    class Runner
    {
        private static GroupedParameters GroupAndFilter(List<ComparedParameter> parameters, string idStarts)
        {
            var groupedParams = new GroupedParameters();            
            foreach (ComparisonStatus status in Enum.GetValues(typeof(ComparisonStatus)))
            {
                groupedParams.Add(status, new List<ComparedParameter>());
            }

            var shouldNotFilter = string.IsNullOrEmpty(idStarts);
            foreach (var item in parameters)
            {
                if (shouldNotFilter || item.ID.StartsWith(idStarts))
                {
                    groupedParams[item.Status].Add(item);
                }
            }

            return groupedParams;
        }

        public static void Display(IOptions options, IDisplayImpl impl)
        {
            try {
                var source = Configuration.Reader.Read(options.SourcePath);
                var target = Configuration.Reader.Read(options.TargetPath);
                var comparedParams = Configuration.Analyzer.Compare(source, target);

                var groupedParams = GroupAndFilter(comparedParams, options.IdStartsWith);

                impl.DisplayRecordsInfo(source, target);
                impl.DisplayComparisons(groupedParams, options.Visibility);
            } catch (Exception err) {
                string message = err is Configuration.ReaderPathNotValidException
                    ? err.Message
                    : "Unknown error occured while trying to process your files";

                impl.DisplayError(message);
            }
        }
    }
}
