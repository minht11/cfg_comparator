using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Controllers
{
    [ApiController]
    [Route("configuration")]
    public class Configuration : ControllerBase
    {
        private readonly ILogger<Configuration> _logger;

        public Configuration(ILogger<Configuration> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [Route("compare")]
        public string Post()
        {
            return "Compare";
        }
    }
}
