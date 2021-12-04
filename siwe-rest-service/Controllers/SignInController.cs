using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

using siwe.Messages;

namespace siwe_rest_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignInController : ControllerBase
    {
        // POST api/<SignInController>
        [HttpPost]
        public IActionResult Post([FromBody] SiweMessage message)
        {
            SiweMeResult result =
                new SiweMeResult() { Address = message.Address, Text = message.ToMessage(), Ens = String.Empty };

            return CreatedAtAction(nameof(Post), new { id = message.Address }, result);
        }
    }
}
