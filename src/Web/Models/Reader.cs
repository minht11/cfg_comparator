using System.Linq;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System;
using System.Threading;
using Microsoft.AspNetCore.Http;
using Cfg.ConfigUI;
using Cfg.Configuration;

namespace Web.Models
{
    using InputConstants = Cfg.ConfigUI.UserInput.Constants;

    public class Reader : IReader
    {
        private readonly ManualResetEvent _mrs = new(false);

        private readonly Queue<ReaderInput> _queue = new();

        public RunnerStates Read(out string inputMessage)
        {
            _mrs.WaitOne();
            var inputFromQueue = _queue.Dequeue();
            
            var baseInput = $"{inputFromQueue.SourcePath} {inputFromQueue.TargetPath} ";

            if (inputFromQueue.FilterByStatus != null)
            {
                var filterList = string.Join(",", inputFromQueue.FilterByStatus);
                baseInput += $" {InputConstants.FilterByStatus}={filterList}";
            }

            baseInput += $" {InputConstants.Starts}{inputFromQueue.IdStartsWith}";

            inputMessage = baseInput;

            return RunnerStates.Ok;
        }

        public void AppendMessage(ReaderInput message)
        {
            _queue.Enqueue(message);
            _mrs.Set();
        }
    }
}
