﻿using System;
using System.Collections.Generic;
using System.Linq;
using Cfg.Config;
using Cfg.Models;

namespace Cfg.ConfigCli
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
            return err is Config.ReaderNotValidFile
                ? err.Message
                : "Unknown error occured while trying to process your files";
        }

        private void Write(Input.Result options)
        {
            try
            {
                var source = Config.Reader.Read(options.SourcePath);
                var target = Config.Reader.Read(options.TargetPath);
                var parameters = Config.Analyzer.Compare(source, target);
                var filteredParams = Filter(parameters, options);

                var result = new ComparisonResult() {
                    SourceInfo = ComparisonResult.ConfigInfo.Create(source),
                    TargetInfo = ComparisonResult.ConfigInfo.Create(target),
                    Parameters = filteredParams,
                };

                _writer.Write(new Result<ComparisonResult>() {
                    Data = result,
                });
            }
            catch (Exception err)
            {
                _writer.Write(new Result<ComparisonResult>() {
                    Message = GetErrorMessage(err),
                });
            }
        }

        public void Start()
        {
            while (true)
            {
                var (nextAction, value) = _reader.Read();
    
                if (nextAction == RunnerAction.Exit) break;

                var compareAndExit = nextAction == RunnerAction.CompareAndExit;
                if (nextAction == RunnerAction.Compare || compareAndExit)
                {
                    var parsedInput = Input.Parser.Parse(value ?? "");
                    Write(parsedInput);
                }

                if (compareAndExit)
                {
                    break;
                }
            }
        }
    }
}