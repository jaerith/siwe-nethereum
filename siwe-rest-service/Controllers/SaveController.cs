using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

using Nethereum.Siwe.Core;

using siwe;
using siwe.Messages;

using siwe_rest_service.Models;

namespace siwe_rest_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaveController : Controller
    {
        // PUT api/<SaveController>/5
        [Authorize]
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

            var regexSiweMsg = SiweMessageParser.Parse(msg);
            var abnfSiweMsg  = SiweMessageParser.ParseUsingAbnf(msg);

            if (regexSiweMsg.Address != abnfSiweMsg.Address)
            {
                return UnprocessableEntity("ERROR!  Corrupted SIWE data in session.");
            }

            if (regexSiweMsg.Address != message.Address)
            {
                return UnprocessableEntity("ERROR!  Incorrect session associated with address.");
            }

            try
            {
                regexSiweMsg.CheckExpirationDate();
            }
            catch (ExpiredMessageException ex)
            {
                return Unauthorized("Expiration of session.  Requires another login via SIWE.");
            }

            TempData["siwe"] = msg;

            message.SaveText();

            return NoContent();
        }
    }
}
