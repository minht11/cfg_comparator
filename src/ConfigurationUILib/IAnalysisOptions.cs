using System.Collections.Generic;

namespace CfgComparator.ConfigurationUILib
{
    public interface IAnalysisOptions
    {
      List<Configuration.ComparisonStatus> Visible { get; set; }
      string KeyStartsWith { get; set; }
    }
}
