using System.Collections.Generic;
using CfgComparator.Configuration;

namespace CfgComparator.ConfigUI
{
    interface IDisplayImpl
    {
        void DisplayRecordsInfo(Record source, Record target);

        void DisplayComparisons(List<ComparedParameter> parameters, List<ComparisonStatus> visible);

        void DisplayError(string message);
    }
}
