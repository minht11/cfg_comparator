using System;
using System.Collections.Generic;

namespace CfgComparator.Configuration
{
    public class Analyzer
    {
        /// <summary>
        /// Compares two configuration <see cref="CfgComparator.Configuration.Record" />s.
        /// </summary>
        /// <param name="source">The source configuration <see cref="CfgComparator.Configuration.Record" /></param>
        /// <param name="source">The target configuration <see cref="CfgComparator.Configuration.Record" /></param>
        /// <exception cref="System.ArgumentNullException">Source and/or target params cannot be null</exception>
        public static List<ComparedParameter> Compare(Record source, Record target)
        {
            if (source == null || target == null)
            {
                throw new ArgumentNullException("Source and/or target params cannot be null");
            }

            var sourceParams = source.Parameters;
            // Do not modify original list.
            var targetParams = new List<Parameter>(target.Parameters);

            List<ComparedParameter> comparedParameters = new();

            foreach (var sourceParam in sourceParams)
            {
                var sourceID = sourceParam.ID;
                var targetParam = FindAndRemoveFromList(targetParams, sourceID);
                var (status, changedValue) = CompareParameters(sourceParam, targetParam);

                comparedParameters.Add(new(status, sourceID, sourceParam.Value, changedValue));
            }

            foreach (var targetParam in targetParams)
            {
                comparedParameters.Add(new(ComparisonStatus.Added, targetParam.ID, targetParam.Value, null));
            }

            return comparedParameters;
        }

        private static Parameter? FindAndRemoveFromList(List<Parameter> list, string id)
        {
            var foundParam = list.Find((t) => t.ID == id);
            if (foundParam != null)
            {
                list.Remove(foundParam);
            }
            return foundParam;
        }
        
        private static (ComparisonStatus, string?) CompareParameters(Parameter source, Parameter? target)
        {
            return target switch
            {
                null => (ComparisonStatus.Removed, null),
                { Value: var value } when value != source.Value => (ComparisonStatus.Modified, value),
                _ => (ComparisonStatus.Unchanged, null),
            };
        }
    }
}
