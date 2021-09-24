namespace CfgComparator.UI.Analysis
{
    public abstract class Analyzer
    {
        protected Analyzer? successor;
        public void SetSuccessor(Analyzer successor)
        {
            this.successor = successor;
        }
        public abstract AnalysisOutputOptions? GetOptions(Configuration.ComparisonStatus status);
    }
}