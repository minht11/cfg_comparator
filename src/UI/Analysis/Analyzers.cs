using System;
using CfgComparator.Configuration;

namespace CfgComparator.UI.Analysis
{
    public class Unchanged : Analyzer
    {
        public override AnalysisOutputOptions? GetOptions(ComparisonStatus status)
        {
            if (status == ComparisonStatus.Unchanged) {
                return new() {
                    Color = ConsoleColor.Gray,
                };
            }

            return successor?.GetOptions(status);
        }
    }

    public class Modified : Analyzer
    {
        public override AnalysisOutputOptions? GetOptions(ComparisonStatus status)
        {
            if (status == ComparisonStatus.Modified) {
                return new() {
                    Color = ConsoleColor.Yellow,
                    ShowChangedValue = true,
                };
            }

            return successor?.GetOptions(status);
        }
    }

    public class Removed : Analyzer
    {
        public override AnalysisOutputOptions? GetOptions(ComparisonStatus status)
        {
            if (status == ComparisonStatus.Removed) {
                return new() {
                    Color = ConsoleColor.Red,
                };
            }

            return successor?.GetOptions(status);
        }
    }

    public class Added : Analyzer
    {
        public override AnalysisOutputOptions? GetOptions(ComparisonStatus status)
        {
            if (status == ComparisonStatus.Added) {
                return new() {
                    Color = ConsoleColor.Green,
                };
            }

            return successor?.GetOptions(status);
        }
    }
}