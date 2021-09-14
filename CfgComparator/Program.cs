using System;

namespace CfgComparator
{
    class Program
    {
        static void Main(string[] args)
        {
            String sourcePath = "./test-data/FMB920-default.cfg";
            String targetPath = "./test-data/FMB920-modified.cfg";

            CfgReader reader = new();
            reader.Read(sourcePath); 
        }
    }
}
