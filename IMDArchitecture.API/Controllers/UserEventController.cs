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
    [Route("userEvent")]
    public class UserEventController : ControllerBase
    {
        private readonly IUserEventRepository _database;
        private readonly ILogger<UserEventController> _logger;
        public UserEventController(ILogger<UserEventController> logger, IUserEventRepository database)
        {
            _database = database;
            _logger = logger;
        }


        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ViewUserEvent>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Get() =>
            Ok((await _database.GetAllUserEvents())
                .Select(ViewUserEvent.FromModel).ToList());


        [HttpPost("/event/enrole")]
        [ProducesResponseType(typeof(ViewUserEvent), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateUserEvent(CreateUserEvent UserEvent)
        {
            try
            {
                _logger.LogInformation("Create a new userEvent");
                var createdUserEvent = UserEvent.ToUserEvent();
                var persistedUserEvent = await _database.CreateUserEvent(createdUserEvent);
                return new CreatedResult("/", null);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("{UserEventId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteUserEventById(int UserEventId)
        {
            try
            {
                var userEventId = await _database.GetUserEventById(UserEventId);
                if (userEventId != null)
                {
                    await _database.DeleteUserEvent(userEventId);
                    return NoContent();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Got an error for {nameof(DeleteUserEventById)}");
                return BadRequest(ex.Message);
            }
        }

    }

}