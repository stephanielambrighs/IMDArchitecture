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
        private readonly IEventRepository _database;
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
        public async Task<IActionResult> GetEventById(int EventId)
        {
            try
            {
                var Event = await _database.GetEventById(EventId);
                if (Event != null)
                {
                    return Ok(ViewEvent.FromModel(Event));
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


        [HttpGet("getEventByAge/{age}")]
        [ProducesResponseType(typeof(ViewEvent), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetEventByAge(int age)
        {
            try
            {
                var eventAge = await _database.GetEventByAge(age);
                if (eventAge != null)
                {
                    return Ok(eventAge);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Got an error for {nameof(GetEventByAge)}");
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("{EventId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteEventById(int EventId)
        {
            try
            {
                var Event = await _database.GetEventById(EventId);
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


        [HttpPut("/event/{EventId}")]
        [ProducesResponseType(typeof(ViewUser), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateEvent(UpdateEvent Event)
        {
            try
            {
                var editEvent = Event.updateEvent();
                var persistedEvent = await _database.UpdateEvent(editEvent);
                return new CreatedResult("/", null);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("/event")]
        [ProducesResponseType(typeof(ViewEvent), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateEvent(CreateEvent Event)
        {
            try
            {
                _logger.LogInformation("Create a new event");
                var createdEvent = Event.ToEvent();
                var persistedEvent = await _database.CreateEvent(createdEvent);
                return new CreatedResult("/", null);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }

}