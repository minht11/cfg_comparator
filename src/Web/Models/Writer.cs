using System.Threading;
using System.Collections.Generic;
using Cfg.Configuration;
using Cfg.ConfigUI;
using Cfg.Interfaces;

namespace Web.Models
{
    public class Writer : IWriter
    {
        private readonly ManualResetEvent _mrs = new(false);

        private readonly Queue<IResult<ComparisonResult>> _queue = new();

        public void Write(IResult<ComparisonResult> comparisonResult)
        {
            _mrs.Set();
            _queue.Enqueue(comparisonResult);
        }

        public IResult<ComparisonResult> GetResult()
        {
            _mrs.WaitOne();
            return _queue.Dequeue();
        }
    }
}
