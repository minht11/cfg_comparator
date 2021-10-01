using System.Collections.Generic;
using CfgComparator.Configuration;

namespace CfgComparator.ConfigUI
{
    interface IDisplayImpl
    {
        void DisplayRecordsInfo(Record source, Record target);

        void DisplayComparisons(Dictionary<ComparisonStatus, List<ComparedParameter>> groupedParams, List<ComparisonStatus> visible);
    }
}
