using System.Collections.Generic;

namespace CfgComparator.Configuration
{
    public class Analyzer
    {
        // TODO. This comment is not useful.
        /// <summary>
        /// Compares two configuration <see cref="CfgComparator.Configuration.Record" />s.
        /// </summary>
        /// <param name="source">The source configuration <see cref="CfgComparator.Configuration.Record" /></param>
        /// <param name="source">The target configuration <see cref="CfgComparator.Configuration.Record" /></param>
        public static List<ComparedParameter> Compare(Record source, Record target)
        {
            var sourceParams = source.Parameters;
            // Do not modify original list.
            var targetParams = new List<Parameter>(target.Parameters);

            List<ComparedParameter> comparedParameters = new();

            foreach (var sourceParam in sourceParams)
            {
                var sourceID = sourceParam.ID;
                var sourceValue = sourceParam.Value;

                var targetValue = targetParams.Find((t) => t.ID == sourceID);
                if (targetValue != null)
                {
                    targetParams.Remove(targetValue);
                }

                var (status, changedValue) = targetValue switch
                {
                    null => (ComparisonStatus.Removed, null),
                    { Value: var value } when value != sourceValue => (ComparisonStatus.Modified, value),
                    _ => (ComparisonStatus.Unchanged, null),
                };

                comparedParameters.Add(new(status, sourceID, sourceValue, changedValue));
            }

            foreach (var targetParam in targetParams)
            {
                comparedParameters.Add(new(ComparisonStatus.Added, targetParam.ID, targetParam.Value, null));
            }

            return comparedParameters;
        }
    }
}
