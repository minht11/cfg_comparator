using System;
using System.Linq;
using System.Collections.Generic;

namespace CfgComparator
{
    class CfgAnalysis
    {
        public class Result
        {
            public Dictionary<int, string> Unchanged { get; set; } = new();
            public Dictionary<int, Tuple<string, string>> Modified { get; set; } = new();
            public Dictionary<int, string> Added { get; set; } = new();
            public Dictionary<int, string> Removed { get; set; } = new();

        }
        static public Result Analyse(CfgRecord source, CfgRecord target)
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
                    if (targetValue == sourcePair.Value) {
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
