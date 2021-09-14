using System.Collections.Generic;

namespace CfgComparator
{
    class CfgRecord
    {
        public Dictionary<string, string> Info { get; set; } = new();
        public Dictionary<int, string> Parameters { get; set; } = new();
    }
}
