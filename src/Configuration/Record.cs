using System.Collections.Generic;

namespace CfgComparator.Configuration
{
    public class Record
    {
        
        /// <summary>
        /// Filename of the configuration file from which data was read.
        /// </summary>
        public string Filename { get; }
        
        /// <summary>
        /// Information about the device itself which is not used to configure the device itself.
        /// </summary>
        public List<Parameter> Info { get; } = new();
        
        /// <summary>
        /// Parameters list storing various device configuration options.
        /// </summary>
        public List<Parameter> Parameters { get; } = new();

        public Record(string filename)
        {
            Filename = filename;
        }
    }
}
