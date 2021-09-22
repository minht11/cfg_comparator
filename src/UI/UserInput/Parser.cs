using System.Collections.Generic;
using System.Linq;

namespace CfgComparator.UI.UserInput
{
    public class Parser
    {
        static private Configuration.ComparisonStatus? ParseStatus(string? value) =>
        value switch {
            Constants.unchanged => Configuration.ComparisonStatus.Unchanged,
            Constants.modified => Configuration.ComparisonStatus.Modified,
            Constants.added => Configuration.ComparisonStatus.Added,
            Constants.removed => Configuration.ComparisonStatus.Removed,
            _ => null,
        };

        static public Result Parse(string inputValue)
        {
            var input = new List<string>(inputValue.Split(' '));
            var sourcePath = input.ElementAtOrDefault(0) ?? "";
            var targetPath = input.ElementAtOrDefault(1) ?? "";
            
            Result parsedOptions = new() {
                SourcePath = sourcePath,
                TargetPath = targetPath,
            };

            if (input.Count > 2)
            {
                input.RemoveRange(0, 2);
            }

            foreach (var value in input)
            {
                if (ParseStatus(value) is Configuration.ComparisonStatus status)
                {
                    parsedOptions.Visible.Add(status);
                } else if (value.StartsWith(Constants.starts))
                {
                    parsedOptions.KeyStartsWith = value.Split('=')?[1] ?? "";
                }
            }

            return parsedOptions;
        }
    }
}
