using System.Diagnostics.CodeAnalysis;

namespace Cfg.ConfigCli
{
    public class ComparisonResult
    {
        [MemberNotNullWhen(true, nameof(Data))]
        public bool IsSuccess() => Data != null;

        public string Message { get; init; } = "";

        public Comparison? Data { get; init; }
    }
}
