using System.Collections.Generic;
using System.Linq;

namespace CfgComparator.ConfigurationUILib.UserInput
{
    public class Parser
    {
        static private Configuration.ComparisonStatus? ParseStatus(string? value) =>
        value switch {
            Constants.Unchanged => Configuration.ComparisonStatus.Unchanged,
            Constants.Modified => Configuration.ComparisonStatus.Modified,
            Constants.Added => Configuration.ComparisonStatus.Added,
            Constants.Removed => Configuration.ComparisonStatus.Removed,
            _ => null,
        };

        static private bool TryParsingKeyStarts(string inputValue, out string value)
        {
            var isValid = inputValue.StartsWith(Constants.Starts);
    
            value = isValid ? (inputValue.Split('=')?[1] ?? "") : "";
            return isValid;
        }

        static public Result Parse(string inputValue)
        {
            var input = new List<string>(inputValue.Split(' '));
            var sourcePath = input.ElementAtOrDefault(0) ?? "";
            var targetPath = input.ElementAtOrDefault(1) ?? "";
            
            Result parsedOptions = new() {
                SourcePath = sourcePath,
                TargetPath = targetPath,
            };

            if (input.Count <= 2)
            {
                return parsedOptions;
            }
            input.RemoveRange(0, 2);

            foreach (var value in input)
            {
                if (ParseStatus(value) is Configuration.ComparisonStatus status)
                {
                    parsedOptions.Visible.Add(status);
                } else if (TryParsingKeyStarts(value, out var starts))
                {
                    parsedOptions.KeyStartsWith = starts;
                }
            }

            return parsedOptions;
        }
    }
}
