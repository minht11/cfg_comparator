using System.Collections.Generic;

namespace Cfg.Configuration
{
    /// <summary>
    /// Holds parsed configuration file data.
    /// </summary>
    public class Record
    {
        /// <summary>
        /// Filename of the configuration file from which data was read.
        /// </summary>
        public string FileName { get; }
        
        /// <summary>
        /// Information about the device.
        /// This data is not used to configure the device itself.
        /// </summary>
        public List<Parameter> Attributes { get; } = new();
        
        /// <summary>
        /// List of various device preferences,
        /// each option is used to control device functionality.
        /// </summary>
        public List<Parameter> Parameters { get; } = new();

        public Record(string fileName)
        {
            FileName = fileName;
        }
    }
}
