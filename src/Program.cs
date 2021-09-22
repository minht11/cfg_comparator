namespace CfgComparator
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = UI.Input.GetUserInput();

            var source = Configuration.Reader.Read(options.SourcePath);
            var target = Configuration.Reader.Read(options.TargetPath);
            var analysis = Configuration.Analyzer.Compare(source, target);
            
            ResultsUI.DisplayInfo(source, "Source");
            ResultsUI.DisplayInfo(target, "Target");

            ResultsUI.DisplayAnalysis(analysis, options.Visible, options.KeyStartsWith);
        }
    }
}
