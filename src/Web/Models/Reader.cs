using System.Collections.Generic;
using System.Threading;
using Cfg.ConfigUI;

namespace Web.Models
{
    using InputConstants = Cfg.ConfigUI.Input.Constants;

    public class Reader : IReader
    {
        private readonly ManualResetEvent _mrs = new(false);

        private readonly Queue<ReaderInput> _queue = new();

        private static string GetFilterByStatusArg(List<string>? status)
        {
            if (status == null) {
                return "";
            }

            var combinedStatus = string.Join(',', status);

            if (string.IsNullOrEmpty(combinedStatus))
            {
                return "";
            }

            return $" {InputConstants.FilterByStatus}{combinedStatus}";
        }

        public (RunnerAction, string?) Read()
        {
            _mrs.WaitOne();
            var inputFromQueue = _queue.Dequeue();
            
            var pathsArg = $"{inputFromQueue.SourcePath} {inputFromQueue.TargetPath}";

            var statusArg = GetFilterByStatusArg(inputFromQueue.FilterByStatus);
            var idStartsWithArg = $"{InputConstants.Starts}{inputFromQueue.IdStartsWith}";

            var formatedInput = $"{pathsArg} {statusArg} {idStartsWithArg}";

            return (RunnerAction.CompareAndExit, formatedInput);
        }

        public void AppendMessage(ReaderInput message)
        {
            _queue.Enqueue(message);
            _mrs.Set();
        }
    }
}
