using Cfg.Interfaces;

namespace Web.Models
{
    public class Result<T> : IResult<T>
    {
        public bool IsSuccess() => Data != null;

        public string Message { get; init; } = "";

        public T? Data { get; init; }
    }
}
