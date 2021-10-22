using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IMDArchitecture.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Event> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new Event
            {
                Date = rng.Next(-20, 55),
                Name = "jan",
                Description = Summaries[rng.Next(Summaries.Length)],
            })
            .ToArray();
        }
        [HttpDelete]
        public IEnumerable<Event> Delete()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new Event
            {
                Date = rng.Next(-20, 55),
                Name = "jan",
                Description = Summaries[rng.Next(Summaries.Length)],
            })
            .ToArray();
        }
        [HttpPut]
        public IEnumerable<Event> Put()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new Event
            {
                Date = rng.Next(-20, 55),
                Name = "jan",
                Description = Summaries[rng.Next(Summaries.Length)],
            })
            .ToArray();
        }
    }
}