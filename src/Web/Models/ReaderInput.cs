using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Web.Models
{
    public class ReaderInput
    {
        public string? SourcePath { get; set; }

        public string? TargetPath { get; set; }

        public List<string>? FilterByStatus { get; set; }

        public string? IdStartsWith { get; set; }
    }
}
