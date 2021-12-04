using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

using siwe.Messages;

namespace siwe_rest_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeController : ControllerBase
    {
        // GET api/<MeController>/5
        [HttpGet()]
        public SiweMeResult Get()
        {
            return new SiweMeResult();
        }
    }
}
