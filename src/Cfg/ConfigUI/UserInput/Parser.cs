using System;
using System.Collections.Generic;
using System.Linq;
using Cfg.Configuration;

namespace Cfg.ConfigUI.UserInput
{
    public class Parser
    {
        static private bool ParseStatus(string inputValue, out List<Configuration.ComparisonStatus>? filterByStatus) {
            var notFound = !inputValue.StartsWith(Constants.FilterByStatus);
    
            if (notFound)
            {
                filterByStatus = null;
                return false;
            }

            var rawList = inputValue.Split('=')?[1] ?? "";
            
            filterByStatus = new List<ComparisonStatus>();

            foreach (var value in rawList.Split(','))
            {
                ComparisonStatus? status = value switch {
                    Constants.Unchanged => Configuration.ComparisonStatus.Unchanged,
                    Constants.Modified => Configuration.ComparisonStatus.Modified,
                    Constants.Added => Configuration.ComparisonStatus.Added,
                    Constants.Removed => Configuration.ComparisonStatus.Removed,
                    _ => null,
                };

                if (status is ComparisonStatus s)
                {
                    filterByStatus.Add(s);
                }
            }

            return true;
        }

        static private bool TryParsingKeyStarts(string inputValue, out string value)
        {
            var isValid = inputValue.StartsWith(Constants.Starts);
    
            value = isValid ? (inputValue.Split('=')?[1] ?? "") : "";
            return isValid;
        }

        static public Result Parse(string inputValue)
        {
            var input = new List<string>(inputValue.Trim().Split(' '));
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
                if (ParseStatus(value, out var filterByStatus))
                {
                    parsedOptions.Visibility = filterByStatus;
                } else if (TryParsingKeyStarts(value, out var starts))
                {
                    parsedOptions.IdStartsWith = starts;
                }
            }

            return parsedOptions;
        }
    }
}
