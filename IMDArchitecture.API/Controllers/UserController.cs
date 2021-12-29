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
        private readonly IUserRepository _database;
        private readonly ILogger<UserController> _logger;
        public UserController(ILogger<UserController> logger, IUserRepository database)
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
        public async Task<IActionResult> GetUserById(int UserId)
        {
            try
            {
                var User = await _database.GetUserById(UserId);
                if (User != null)
                {
                    return Ok(ViewUser.FromModel(User));
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Got an error for {nameof(GetUserById)}");
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("{UserId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteUserById(int UserId)
        {
            try
            {
                var User = await _database.GetUserById(UserId);
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


        [HttpPut("/user/{UserId}")]
        [ProducesResponseType(typeof(ViewUser), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateUser(UpdateUser User)
        {
            try
            {
                var editUser = User.updateUser();
                var persistedUser = await _database.UpdateUser(editUser);
                return new CreatedResult("/", null);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("/user")]
        [ProducesResponseType(typeof(ViewUser), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateUser(CreateUser User)
        {
            try
            {
                var createUser = User.ToUser();
                var persistedUser = await _database.CreateUser(createUser);
                return new CreatedResult("/", null);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}