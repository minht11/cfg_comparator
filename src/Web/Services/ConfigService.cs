using Microsoft.Extensions.Logging;
using Web.Models;
using Web.Interfaces;
using Cfg.ConfigCli;

namespace Web.Services
{
    public class ConfigService : IConfigService
    {
        private readonly ILogger<ConfigService> _logger;

        private readonly Reader _reader;

        private readonly Writer _writer;

        private readonly Runner _runner;

        public ConfigService(ILogger<ConfigService> logger)
        {
            _logger = logger;
            _reader = new();
            _writer = new();
            _runner =  new(_reader, _writer);
        }

        public ComparisonResult CompareAndFilter(InputOptions input)
        {
            _reader.AppendMessage(input);
            _runner.Start();
            return _writer.GetResult();
        }
    }
}
