using System;
using System.Linq;
using System.Collections.Generic;

namespace CfgComparator.Configuration
{
    public class Analysis
    {
        /// <summary>
        /// Two <see cref="CfgComparator.Configuration.Record" />s analysis result.
        /// </summary>
        public class Result
        {
            public Dictionary<int, string> Unchanged { get; set; } = new();
            public Dictionary<int, Tuple<string, string>> Modified { get; set; } = new();
            public Dictionary<int, string> Added { get; set; } = new();
            public Dictionary<int, string> Removed { get; set; } = new();
        }
        
        /// <summary>
        /// Compares two configuration <see cref="CfgComparator.Configuration.Record" />s and gives analysis about them.
        /// </summary>
        /// <param name="source">The source configuration <see cref="CfgComparator.Configuration.Record" /></param>
        /// <param name="source">The target configuration <see cref="CfgComparator.Configuration.Record" /></param>
        static public Result Analyse(Record source, Record target)
        {
            var sourceParams = source.Parameters;
            // Do not modify original list.
            var targetParams = new Dictionary<int, string>(target.Parameters);

            Result result = new();

            foreach (var sourcePair in sourceParams)
            {
                var sourceKey = sourcePair.Key;
                var sourceValue = sourcePair.Value;
                if (targetParams.ContainsKey(sourceKey))
                {
                    var targetValue = targetParams[sourceKey];
                    if (targetValue == sourceValue) {
                        result.Unchanged.Add(sourceKey, sourceValue);
                    } else {
                        result.Modified.Add(sourceKey, Tuple.Create(sourceValue, targetValue));
                    }
                    targetParams.Remove(sourceKey);
                } else {
                    result.Removed.Add(sourceKey, sourceValue);
                }
            }
            result.Added = targetParams;

            return result;
        }
    }
}
