using System;

namespace Cfg.ConfigCliFrontend
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

        static protected void Display(string message)
        {
            Console.WriteLine(message);
        }
    }
}
