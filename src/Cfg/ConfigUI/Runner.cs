using System;
using System.Collections.Generic;
using System.Linq;
using Cfg.Configuration;

namespace Cfg.ConfigUI
{
    public class Runner
    {
        private IReader _reader;

        private IWriter _writer;

        public Runner(IReader reader, IWriter writer)
        {
            _reader = reader;
            _writer = writer;
        }

        private static List<ComparedParameter> Filter(List<ComparedParameter> parameters, IFilterOptions options)
        {
            var visibility = options.Visibility;
            var idStartsWith = options.IdStartsWith;

            var shouldNotFilterById = string.IsNullOrEmpty(idStartsWith);

            return parameters.Where((param) => {
                var statusMatch = visibility == null || visibility.Contains(param.Status);
                var idMatch = shouldNotFilterById || param.ID.StartsWith(idStartsWith!);

                return statusMatch && idMatch;
            }).ToList();
        }

        private static string GetErrorMessage(Exception err)
        {
            return err is Configuration.ReaderNotValidFile
                ? err.Message
                : "Unknown error occured while trying to process your files";
        }

        public void Start() 
        {
            var options = _reader.Read();

            try {
                var source = Configuration.Reader.Read(options.SourcePath);
                var target = Configuration.Reader.Read(options.TargetPath);
                var parameters = Configuration.Analyzer.Compare(source, target);
                var filteredParams = Filter(parameters, options);

                _writer.Write(filteredParams);
            }
            catch (Exception err)
            {
                _writer.WriteException(GetErrorMessage(err));
            }
        }
    }
}
