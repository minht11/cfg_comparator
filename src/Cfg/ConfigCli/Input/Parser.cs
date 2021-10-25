using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics.CodeAnalysis;
using Cfg.Config;

namespace Cfg.ConfigCli.Input
{
    public class Parser
    {
        static private bool ParseStatus(string inputValue, [NotNullWhen(true)] out List<Config.ComparisonStatus>? filterByStatus) {
            var notFound = !inputValue.StartsWith(Constants.StatusArg);
    
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
                    Constants.Unchanged => Config.ComparisonStatus.Unchanged,
                    Constants.Modified => Config.ComparisonStatus.Modified,
                    Constants.Added => Config.ComparisonStatus.Added,
                    Constants.Removed => Config.ComparisonStatus.Removed,
                    _ => null,
                };

                if (status is ComparisonStatus s)
                {
                    filterByStatus.Add(s);
                }
            }

            return true;
        }

        static private bool TryParsingKeyStarts(string inputValue, [NotNullWhen(true)] out string value)
        {
            var isValid = inputValue.StartsWith(Constants.StartsArg);
    
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