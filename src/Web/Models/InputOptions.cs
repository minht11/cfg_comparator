using System.Collections.Generic;

namespace Web.Models
{
    public class InputOptions
    {
        public string SourcePath { get; init; } = "";

        public string TargetPath { get; init; } = "";

        public List<string>? FilterByStatus { get; init; }

        public string? IdStartsWith { get; init; }
    }
}
