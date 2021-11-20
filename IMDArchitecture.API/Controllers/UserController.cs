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
    [Route("user")]
    public class UserController : ControllerBase
    {
        // noticce we don't care about our actual database implementation; we just pass an interface (== contract)
        private readonly IDatabase _database;

        // everything you use on _logger will end up on STDOUT (the terminal where you started your process)
        private readonly ILogger<UserController> _logger;

        // This is called dependency injection; it makes it very easy to test this class as you don't "hardwire" a database in the
        // test; you pass an interface containing a certain amount of methods. This will become clearer in the following lessons.
        public UserController(ILogger<UserController> logger, IDatabase database)
        {
            _database = database;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ViewUser>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Get() =>
            Ok((await _database.GetAllUsers())
                .Select(ViewUser.FromModel).ToList());

        [HttpGet("{UserId}")]
        [ProducesResponseType(typeof(ViewUser), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserById(string UserId)
        {
            try
            {
                var User = await _database.GetUserById(Guid.Parse(UserId));
                if (User != null)
                {
                    return Ok(ViewUser.FromModel(User));//event
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Got an error for {nameof(GetUserById)}");
                // This is just good practice; you never want to expose a raw exception message. There are some libraries/services to handle this
                // but it's better to take full control of your code.
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{UserId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteUserById(string UserId)
        {
            try
            {
                var parsedId = Guid.Parse(UserId);
                var User = await _database.GetUserById(parsedId);
                if (User != null)
                {
                    await _database.DeleteUser(User);
                    return NoContent();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Got an error for {nameof(DeleteUserById)}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPut()]
        [ProducesResponseType(typeof(ViewUser), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateUser(CreateUser User)
        {
            try
            {
                var createdUser = User.ToUser();
                var persistedUser = await _database.UpdateUser(createdUser);
                return CreatedAtAction(nameof(GetUserById), new { id = createdUser.UserId.ToString() }, ViewUser.FromModel(persistedUser));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Got an error for {nameof(UpdateUser)}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("/createUser")]
        [ProducesResponseType(typeof(ViewUser), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateUser(CreateUser User)
        {
            try
            {
                _logger.LogInformation("Create a new user");
                var createUser = User.ToUser();
                var persistedUser = await _database.CreateUser(createUser);
                return CreatedAtAction(nameof(GetUserById), new { id = createUser.UserId.ToString() }, ViewUser.FromModel(persistedUser));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}