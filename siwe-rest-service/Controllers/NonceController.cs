using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

using siwe.Messages;

namespace siwe_rest_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NonceController : Controller
    {
        [HttpGet()]
        public string GetNonce()
        {
            string nonce = SiweMessage.GetNonce();

            TempData["nonce"] = nonce;
            // HttpContext.Session.SetString("nonce", nonce);

            return nonce;
        }
    }
}
