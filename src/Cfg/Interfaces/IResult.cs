using System.Diagnostics.CodeAnalysis;

namespace Cfg.Interfaces
{
    public interface IResult<out T>
    {
        [MemberNotNullWhen(true, nameof(Data))]
        bool IsSuccess();

        string Message { get; }

        T? Data { get; }
    }
}
