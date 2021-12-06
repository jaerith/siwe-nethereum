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
            /**
             ** NOTE: To be ported
             **
app.get('/api/me', async (req, res) => {
    if (!req.session.siwe) {
        res.status(401).json({ message: 'You have to first sign_in' });
        return;
    }
    res.status(200)
        .json({
            text: getText(req.session.siwe.address),
            address: req.session.siwe.address,
            ens: req.session.ens,
        })
        .end();
});
            **/

            return new SiweMeResult();
        }
    }
}
