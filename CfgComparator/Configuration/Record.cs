using System.Collections.Generic;

namespace CfgComparator.Configuration
{
    class Record
    {
        public string Filename { get; set; } = "";
        public Dictionary<string, string> Info { get; set; } = new();
        public Dictionary<int, string> Parameters { get; set; } = new();
    }
}
