using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebinarManagement.Models;
using WebinarManagement.Services;

namespace WebinarManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAccountController : ControllerBase
    {
        private readonly UserAccountService _userAccountService;

        public UserAccountController(UserAccountService userAccountService)
        {
            _userAccountService = userAccountService;
        }

        [HttpGet("GetUserByUsername/{userName}")]
        public async Task<ActionResult<User>> GetUserByUsername(string userName)
        {
            var user = await _userAccountService.GetUserByUsernameAsync(userName);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet("GetUserWebinars/{userId}")]
        public async Task<ActionResult<List<Webinar>>> GetUserWebinars(int userId)
        {
            var webinars = await _userAccountService.GetUserWebinarsAsync(userId);
            return Ok(webinars);
        }

        [HttpGet("GetSpeakerWebinars/{speakerId}")]
        public async Task<ActionResult<List<Webinar>>> GetSpeakerWebinars(int speakerId)
        {
            var webinars = await _userAccountService.GetSpeakerWebinarAsync(speakerId);
            return Ok(webinars);
        }

        [HttpPost("RegisterUser")]
        public async Task<IActionResult> RegisterUser([FromBody] User newUser, [FromQuery] string role)
        {
            var success = await _userAccountService.RegisterUserAsync(newUser, role);
            if (!success)
            {
                return Conflict("Username already exists.");
            }
            return Ok(newUser);
        }

        [HttpPost("AddParticipant/{userId}/{webinarId}")]
        public async Task<IActionResult> AddParticipant(int userId, int webinarId)
        {
            await _userAccountService.AddParticipantAsync(userId, webinarId);
            return Ok();
        }

        [HttpDelete("RemoveParticipant/{userId}/{webinarId}")]
        public async Task<IActionResult> RemoveParticipant(int userId, int webinarId)
        {
            await _userAccountService.RemoveParticipantAsync(userId, webinarId);
            return NoContent();
        }
    }
}
