using System;

namespace CfgComparator.ConsoleUILib
{
    public class ConsoleUI : BaseUI
    {
        static public void Start(ILifecycle lifecycle)
        {
            while (true)
            {
                lifecycle.DisplayHeader();
                var input = Console.ReadLine() ?? "";

                if (lifecycle.DisplayContent(input))
                {
                    break;
                }
            }
        }
    }
}
