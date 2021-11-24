using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using IMDArchitecture.API.Domain;
using IMDArchitecture.API.Ports;
using IMDArchitecture.API.Models;

namespace IMDArchitecture.API.Controllers
{
    [ApiController]
    [Route("event")]
    public class EventController : ControllerBase
    {
        // noticce we don't care about our actual database implementation; we just pass an interface (== contract)
        private readonly IEventRepository _database;

        // everything you use on _logger will end up on STDOUT (the terminal where you started your process)
        private readonly ILogger<EventController> _logger;

        // This is called dependency injection; it makes it very easy to test this class as you don't "hardwire" a database in the
        // test; you pass an interface containing a certain amount of methods. This will become clearer in the following lessons.
        public EventController(ILogger<EventController> logger, IEventRepository database)
        {
            _database = database;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ViewEvent>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Get() =>
            Ok((await _database.GetAllEvents())
                .Select(ViewEvent.FromModel).ToList());

        [HttpGet("{EventId}")]
        [ProducesResponseType(typeof(ViewEvent), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetEventById(string EventId)
        {
            try
            {
                var Event = await _database.GetEventById(Guid.Parse(EventId));
                if (Event != null)
                {
                    return Ok(ViewEvent.FromModel(Event));//event
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Got an error for {nameof(GetEventById)}");
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{EventId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteEventById(string EventId)
        {
            try
            {
                var parsedId = Guid.Parse(EventId);
                var Event = await _database.GetEventById(parsedId);
                if (Event != null)
                {
                    await _database.DeleteEvent(Event);
                    return NoContent();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Got an error for {nameof(DeleteEventById)}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPut()]
        [HttpPost("/createEvent")]
        [ProducesResponseType(typeof(ViewEvent), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateEvent(CreateEvent Event)
        {
            try
            {
                _logger.LogInformation("Create a new event");
                var createdEvent = Event.ToEvent();
                var persistedEvent = await _database.CreateEvent(createdEvent);
                return CreatedAtAction(nameof(GetEventById), new { id = createdEvent.EventId.ToString() }, ViewEvent.FromModel(persistedEvent));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }

}