using System.Collections.Generic;
using System.Threading;
using Cfg.ConfigCli;

namespace Web.Models
{
    using InputConstants = Cfg.ConfigCli.Input.Constants;

    public class Reader : IReader
    {
        private readonly ManualResetEvent _mrs = new(false);

        private readonly Queue<InputOptions> _queue = new();

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

            return $" {InputConstants.StatusArg}{combinedStatus}";
        }

        public (RunnerAction, string?) Read()
        {
            _mrs.WaitOne();
            var inputFromQueue = _queue.Dequeue();
            
            var pathsArg = $"{inputFromQueue.SourcePath} {inputFromQueue.TargetPath}";

            var statusArg = GetFilterByStatusArg(inputFromQueue.FilterByStatus);
            var idStartsWithArg = $"{InputConstants.StartsArg}{inputFromQueue.IdStartsWith}";

            var formatedInput = $"{pathsArg} {statusArg} {idStartsWithArg}";

            return (RunnerAction.CompareAndExit, formatedInput);
        }

        public void AppendMessage(InputOptions options)
        {
            _queue.Enqueue(options);
            _mrs.Set();
        }
    }
}
