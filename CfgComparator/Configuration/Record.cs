using System.Collections.Generic;

namespace CfgComparator.Configuration
{
    public class Record
    {
        
        /// <summary>
        /// Filename of configuration file from which this data was read.
        /// </summary>
        public string Filename { get; set; } = "";
        
        /// <summary>
        /// Information about the device which is not used to configure the device itself.
        /// </summary>
        public Dictionary<string, string> Info { get; set; } = new();
        
        /// <summary>
        /// Parameters dictionary storing various device options.
        /// </summary>
        public Dictionary<int, string> Parameters { get; set; } = new();
    }
}
