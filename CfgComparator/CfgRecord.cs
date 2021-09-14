using System.Collections.Generic;

namespace CfgComparator
{
    class CfgRecord
    {
        public Dictionary<string, string> info { get; set; } = new();
        public Dictionary<int, string> parameters { get; set; } = new();
    }
}
