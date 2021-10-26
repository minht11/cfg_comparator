using System;

namespace Cfg.ConfigCliFrontend
{
    public abstract class BaseUI
    {
        protected static void DisplaySeparator()
        {
            Console.WriteLine("---------------------------");
        }

        protected static void DisplaySpace()
        {
            Console.WriteLine("");
        }

        protected static void Display(string message)
        {
            Console.WriteLine(message);
        }
    }
}
