using System.Collections.Generic;
using Cfg.Configuration;

namespace Cfg.ConfigUI
{
    public interface IDisplayImpl
    {
        void DisplayRecordsInfo(Record source, Record target);

        void DisplayComparisons(List<ComparedParameter> parameters, List<ComparisonStatus> visible);

        void DisplayError(string message);
    }
}
