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
            var source = reader.Read(sourcePath);
            var target = reader.Read(targetPath);
            var analysis = CfgAnalysis.Analyse(source, target);
            foreach (var a in analysis.Modified)
            {
                Console.WriteLine($"{a.Key} {a.Value}");
            }
        }
    }
}
