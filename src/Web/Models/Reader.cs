using System.Collections.Generic;
using System.Threading;
using Cfg.ConfigUI;

namespace Web.Models
{
    using InputConstants = Cfg.ConfigUI.UserInput.Constants;

    public class Reader : IReader
    {
        private readonly ManualResetEvent _mrs = new(false);

        private readonly Queue<ReaderInput> _queue = new();

        public (Actions, string?) Read()
        {
            _mrs.WaitOne();
            var inputFromQueue = _queue.Dequeue();
            
            var formatedInput = $"{inputFromQueue.SourcePath} {inputFromQueue.TargetPath} ";

            if (inputFromQueue.FilterByStatus != null)
            {
                var filterList = string.Join(",", inputFromQueue.FilterByStatus);
                formatedInput += $" {InputConstants.FilterByStatus}={filterList}";
            }

            formatedInput += $" {InputConstants.Starts}{inputFromQueue.IdStartsWith}";

            return (Actions.CompareAndExit, formatedInput);
        }

        public void AppendMessage(ReaderInput message)
        {
            _queue.Enqueue(message);
            _mrs.Set();
        }
    }
}
