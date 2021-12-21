using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

using Nethereum.Siwe.Core;

using siwe;

namespace siwe_rest_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NonceController : Controller
    {
        [HttpGet()]
        public string GetNonce()
        {
            string nonce = new SiweMessage().GetNonce();

            TempData["nonce"] = nonce;            

            return nonce;
        }
    }
}
