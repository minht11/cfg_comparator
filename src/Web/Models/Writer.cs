using System.Threading;
using System.Collections.Generic;
using Cfg.ConfigCli;

namespace Web.Models
{
    public class Writer : IWriter
    {
        private readonly ManualResetEvent _mrs = new(false);

        private readonly Queue<ComparisonResult> _queue = new();

        public void Write(ComparisonResult comparisonResult)
        {
            _mrs.Set();
            _queue.Enqueue(comparisonResult);
        }

        public ComparisonResult GetResult()
        {
            _mrs.WaitOne();
            return _queue.Dequeue();
        }
    }
}
