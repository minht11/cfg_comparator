using System.Collections.Generic;
using Cfg.Configuration;

namespace Cfg.ConfigUI
{
    public interface IWriter
    {
        void Write(Record source, Record target, List<ComparedParameter> parameters);

        void WriteException(string message);
    }
}
