using System;

namespace CfgComparator.ConsoleUILib
{
    public interface ILifecycle
    {
        void DisplayHeader();

        /// <summary>
        /// Displays main content
        /// </summary>
        /// <returns>Returns true if UI should be stopped, false otherwise</returns>
        bool DisplayContent(string value);
    }
}
