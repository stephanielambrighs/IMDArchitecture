using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RandalsVideoStore.API.Domain;
using RandalsVideoStore.API.Ports;

namespace RandalsVideoStore.API.Controllers
{
    [ApiController]
    [Route("event")]
    public class EventController : ControllerBase
    {
        // noticce we don't care about our actual database implementation; we just pass an interface (== contract)
        private readonly IDatabase _database;

        // everything you use on _logger will end up on STDOUT (the terminal where you started your process)
        private readonly ILogger<EventController> _logger;

        // This is called dependency injection; it makes it very easy to test this class as you don't "hardwire" a database in the
        // test; you pass an interface containing a certain amount of methods. This will become clearer in the following lessons.
        public EventController(ILogger<EventController> logger, IDatabase database)
        {
            _database = database;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ViewEvent>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Get(string titleStartsWith) =>
            Ok((await _database.GetAllEvents(titleStartsWith))
                .Select(ViewEvent.FromModel).ToList());

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ViewEvent), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var event = await _database.GetEventById(Guid.Parse(id));
                if (event != null)
                {
            return Ok(ViewEvent.FromModel(movie));//event
        }
                else
                {
            return NotFound();
        }
        }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Got an error for {nameof(GetById)}");
                // This is just good practice; you never want to expose a raw exception message. There are some libraries/services to handle this
                // but it's better to take full control of your code.
                return BadRequest(ex.Message);
    }
}

[HttpDelete("{id}")]
[ProducesResponseType(StatusCodes.Status204NoContent)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
public async Task<IActionResult> DeleteById(string id)
{
    try
    {
        var parsedId = Guid.Parse(id);
        var movie = await _database.GetEventById(parsedId);
        if (movie != null)
        {
            await _database.DeleteEvent(parsedId);
            return NoContent();
        }
        else
        {
            return NotFound();
        }
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, $"Got an error for {nameof(DeleteById)}");
        return BadRequest(ex.Message);
    }
}

[HttpPut()]
[ProducesResponseType(typeof(ViewEvent), StatusCodes.Status201Created)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public async Task<IActionResult> PersistEvent(CreateEvent event)
{
    try
    {
        var createdEvent = movie.ToMovie();//event
        var persistedEvent = await _database.PersistMovie(createdEvent);
        return CreatedAtAction(nameof(GetById), new { id = createdEvent.Id.ToString() }, ViewEvent.FromModel(persistedEvent));
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, $"Got an error for {nameof(PersistEvent)}");
        return BadRequest(ex.Message);
    }
}
        }

    }