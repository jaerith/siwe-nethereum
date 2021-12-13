using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

using siwe;
using siwe.Messages;

using siwe_rest_service.Models;

namespace siwe_rest_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignInController : Controller
    {
        // POST api/<SignInController>
        [HttpPost]
        public IActionResult Post([FromBody] SiweMessage message)
        {
            string? nonce = String.Empty;

            if (message.Nonce == null)
                return BadRequest("Provided nonce is null");

            if (TempData.ContainsKey("nonce"))
            {
                nonce = (string)TempData["nonce"];
            }
            else if (!String.IsNullOrEmpty(HttpContext.Session.GetString("siwe")))
            {
                /**
                 ** NOTE: Get nonce from stored SIWE message
                 **
                nonce = HttpContext.Session.GetString("siwe");
                 **/
            }

            if ((message == null) || String.IsNullOrEmpty(message.Address) || String.IsNullOrEmpty(message.Signature))
                return UnprocessableEntity("Malformed session");

            try
            {
                if (nonce != message.Nonce)
                    return UnprocessableEntity();

                message.Validate();
            }
            catch (InvalidSignatureException ex)
            {
                return BadRequest("Invalid Signature -> \n" + ex.ToString());
            }
            catch (ExpiredMessageException ex)
            {
                return BadRequest("Expired Message -> \n" + ex.ToString());
            }
            finally
            {
                HttpContext.Session.SetString("siwe",  string.Empty);
                HttpContext.Session.SetString("ens",   string.Empty);
                HttpContext.Session.SetString("nonce", string.Empty);
            }

            HttpContext.Session.SetString("siwe",  message.SignMessage());
            HttpContext.Session.SetString("ens",   string.Empty);
            HttpContext.Session.SetString("nonce", string.Empty);
            // req.session.cookie.expires = new Date(fields.expirationTime);?

            TempData["siwe"] = message.ToMessage();

            SiweMessageAndText result =
                new SiweMessageAndText() { Address = message.Address, Text = message.ToMessage(), Ens = String.Empty };

            return CreatedAtAction(nameof(Post), new { id = message.Address }, result);
        }
    }
}
