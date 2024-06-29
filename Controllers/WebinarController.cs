using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebinarManagement.Models;
using WebinarManagement.Services;

namespace WebinarManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebinarsController : ControllerBase
    {
        private readonly WebinarService _webinarService;

        public WebinarsController(WebinarService webinarService)
        {
            _webinarService = webinarService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Webinar>>> GetWebinars()
        {
            var webinars = await _webinarService.GetWebinarsAsync();
            return Ok(webinars);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Webinar>> GetWebinar(int id)
        {
            var webinar = await _webinarService.GetWebinarByIdAsync(id);
            if (webinar == null)
            {
                return NotFound();
            }
            return Ok(webinar);
        }

        [HttpPost]
        public async Task<ActionResult> AddWebinar([FromBody] Webinar webinar, [FromQuery] int speakerId)
        {
            await _webinarService.AddWebinarAsync(webinar, speakerId);
            return CreatedAtAction(nameof(GetWebinar), new { id = webinar.WebinarID }, webinar);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWebinar(int id, [FromBody] Webinar webinar)
        {
            if (id != webinar.WebinarID)
            {
                return BadRequest();
            }
            await _webinarService.UpdateWebinarAsync(webinar);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWebinar(int id)
        {
            await _webinarService.DeleteWebinarAsync(id);
            return NoContent();
        }
    }
}
