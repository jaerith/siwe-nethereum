using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

using siwe.Messages;

using siwe_rest_service;
using siwe_rest_service.Models;

namespace siwe_rest_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaveController : Controller
    {
        // PUT api/<SaveController>/5
        [HttpPut]
        public IActionResult Update([FromBody] SiweMessageAndText message)
        {
            if ((message == null) || string.IsNullOrEmpty(message.Address))
                return BadRequest();

            string? msg = null;

            if (TempData.ContainsKey("siwe"))
            {
                msg = (string) TempData["siwe"];
            }

            if (msg == null)
            {
                return Unauthorized("You have to first sign-in.");
            }

            TempData["siwe"] = msg;

            message.SaveText();

            return NoContent();
        }
    }
}
