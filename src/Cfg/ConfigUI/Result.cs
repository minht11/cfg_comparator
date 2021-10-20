using System.Diagnostics.CodeAnalysis;
using Cfg.Interfaces;

namespace Cfg.ConfigUI
{
    public class Result : IResult<ComparisonResult>
    {
        public bool IsSuccess() => Data != null;

        public string Message { get; init; } = "";

        public ComparisonResult? Data { get; init; }
    }
}
