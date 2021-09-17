using System.Collections.Generic;

namespace CfgComparator.Configuration
{
    /// <summary>
    /// Holds parsed configuration file data.
    /// </summary>
    public class Record
    {
        
        /// <summary>
        /// Filename of the configuration file from which data was read.
        /// </summary>
        public string Filename { get; }
        
        /// <summary>
        /// Information about the device.
        /// This data is not used to configure the device itself.
        /// </summary>
        public List<Parameter> Info { get; } = new();
        
        /// <summary>
        /// Device configuration options.
        /// Used to configure various preferences.
        /// </summary>
        public List<Parameter> Parameters { get; } = new();

        public Record(string filename)
        {
            Filename = filename;
        }
    }
}
