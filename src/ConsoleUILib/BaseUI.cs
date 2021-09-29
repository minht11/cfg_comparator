using System;

namespace CfgComparator.ConsoleUILib
{
    public abstract class BaseUI
    {
        static protected void DisplaySeparator()
        {
            Console.WriteLine("---------------------------");
        }

        static protected void DisplaySpace()
        {
            Console.WriteLine("");
        }
    }
}
