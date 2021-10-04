using System;
using System.Collections.Generic;
using System.Linq;
using CfgComparator.Configuration;

namespace CfgComparator.ConfigUI
{
    class Runner
    {
        private static List<ComparedParameter> FilterById(List<ComparedParameter> parameters, string idStarts)
        {
            var shouldNotFilter = string.IsNullOrEmpty(idStarts);
            return parameters.Where((param) => shouldNotFilter || param.ID.StartsWith(idStarts)).ToList();
        }

        public static void Display(IOptions options, IDisplayImpl impl)
        {
            try {
                var source = Configuration.Reader.Read(options.SourcePath);
                var target = Configuration.Reader.Read(options.TargetPath);
                var comparedParams = Configuration.Analyzer.Compare(source, target);

                var groupedParams = FilterById(comparedParams, options.IdStartsWith);

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
