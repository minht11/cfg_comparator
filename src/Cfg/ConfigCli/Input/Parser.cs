using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics.CodeAnalysis;
using Cfg.Config;

namespace Cfg.ConfigCli.Input
{
    public class Parser
    {
        private static string GetValueAfterEqual(string value) => value.Split('=')?[1] ?? "";

        private static bool ParseStatus(string inputValue, [NotNullWhen(true)] out List<ComparisonStatus>? filterByStatus)
        {
            var notFound = !inputValue.StartsWith(Constants.StatusArg);
    
            if (notFound)
            {
                filterByStatus = null;
                return false;
            }

            var rawValues = GetValueAfterEqual(inputValue);
            
            filterByStatus = new List<ComparisonStatus>();

            foreach (var value in rawValues.Split(','))
            {
                ComparisonStatus? status = value switch {
                    Constants.Unchanged => ComparisonStatus.Unchanged,
                    Constants.Modified => ComparisonStatus.Modified,
                    Constants.Added => ComparisonStatus.Added,
                    Constants.Removed => ComparisonStatus.Removed,
                    _ => null,
                };

                if (status is ComparisonStatus s)
                {
                    filterByStatus.Add(s);
                }
            }

            return true;
        }

        private static bool TryParsingKeyStarts(string inputValue, [NotNullWhen(true)] out string value)
        {
            var isValid = inputValue.StartsWith(Constants.StartsArg);
    
            value = isValid ? GetValueAfterEqual(inputValue) : "";
            return isValid;
        }

        public static Result Parse(string inputValue)
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
