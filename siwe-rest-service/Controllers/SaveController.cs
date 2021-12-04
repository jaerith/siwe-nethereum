using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

using siwe.Messages;

namespace siwe_rest_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaveController : ControllerBase
    {
        // PUT api/<SaveController>/5
        [HttpPut]
        public IActionResult Update([FromBody] SiweMessage message)
        {
            if ((message == null) || string.IsNullOrEmpty(message.Address))
                return BadRequest();            

            return NoContent();
        }
    }
}
