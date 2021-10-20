﻿using System.Threading.Tasks;
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

        private static ComparisonResult.ConfigInfo CreateConfigInfo(Record record) =>
            new ComparisonResult.ConfigInfo() {
                FileName = record.FileName,
                Attributes = record.Info,
            };

        private void Write(UserInput.Result options)
        {
            try
            {
                var source = Configuration.Reader.Read(options.SourcePath);
                var target = Configuration.Reader.Read(options.TargetPath);
                var parameters = Configuration.Analyzer.Compare(source, target);
                var filteredParams = Filter(parameters, options);

                var result = new ComparisonResult() {
                    SourceInfo = CreateConfigInfo(source),
                    TargetInfo = CreateConfigInfo(target),
                    Parameters = filteredParams,
                };

                _writer.Write(new Result() {
                    Data = result,
                });
            }
            catch (Exception err)
            {
                _writer.Write(new Result() {
                    Message = GetErrorMessage(err),
                });
            }
        }

        public void Start()
        {
            while (true)
            {
                var state = _reader.Read(out var input);
    
                if (state == RunnerStates.Exit) break;

                var isOk = state == RunnerStates.Ok;
                if (state == RunnerStates.OkNext || isOk)
                {
                    var parsedInput = UserInput.Parser.Parse(input);
                    Write(parsedInput);
                }

                if (isOk)
                {
                    break;
                }
            }
        }
    }
}
