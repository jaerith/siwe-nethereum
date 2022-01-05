using Microsoft.AspNetCore.Mvc;

using Nethereum.Siwe.Core;

using siwe;
using siwe.Messages;

using siwe_rest_service.Models;

using siwe_rest_service.Logic;

namespace siwe_rest_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignInController : Controller
    {
        private readonly ITokenLogic _tokenLogic;

        public SignInController(ITokenLogic tokenLogic)
        {
            _tokenLogic = tokenLogic;
        }

        // POST api/<SignInController>
        [HttpPost]
        public IActionResult Post([FromBody] SiweMessage message)
        {
            try
            {
                var tmpCache = new Dictionary<string, string>();

                TempData.ToList().Where(x => x.IsSiweCacheKeyValue())
                        .ToList().ForEach(x => tmpCache[x.Key] = (string)x.Value);

                message.SiweSignIn(tmpCache);

                foreach (string key in tmpCache.Keys)
                {
                    TempData[key] = tmpCache[key];
                }
            }
            catch (NoNonceException ex)
            {
                return BadRequest(ex.ToString());
            }
            catch (InvalidDataException ex)
            {
                return UnprocessableEntity(ex.ToString());
            }
            catch (InvalidSignatureException ex)
            {
                return BadRequest("Invalid Signature -> \n" + ex.ToString());
            }
            catch (ExpiredMessageException ex)
            {
                return BadRequest("Expired Message -> \n" + ex.ToString());
            }
            catch (NotBeforeException ex)
            {
                return BadRequest("Not Before Message -> \n" + ex.ToString());
            }
            finally
            {
                HttpContext.Session.SetString("siwe", string.Empty);
                HttpContext.Session.SetString("ens", string.Empty);
                HttpContext.Session.SetString("nonce", string.Empty);
            }

            HttpContext.Session.SetString("siwe", message.SignMessage());
            HttpContext.Session.SetString("ens", string.Empty);
            HttpContext.Session.SetString("nonce", string.Empty);

            // NOTE: How to handle cookies still needs to be determined
            // req.session.cookie.expires = new Date(fields.expirationTime);?

            SiweMessageAndText result =
                new SiweMessageAndText() { Address = message.Address, Text = message.GetText(), Ens = String.Empty };

            result.Token = _tokenLogic.GetAuthenticationToken(message);

            return CreatedAtAction(nameof(Post), new { id = message.Address }, result);
        }

    }
}
