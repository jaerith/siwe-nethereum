using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

using siwe.Messages;

namespace siwe_rest_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignOutController : Controller
    {
        // POST api/<SignOutController>
        [HttpPost]
        public IActionResult Post([FromBody] SiweMessage message)
        {
            SiweMeResult result =
                new SiweMeResult() { Address = message.Address, Text = message.ToMessage(), Ens = String.Empty };

            /**
             ** NOTE: To be ported
             **
    app.post('/api/sign_out', async (req, res) => {
    if (!req.session.siwe)
    {
        res.status(401).json({ message: 'You have to first sign_in' });
    return;
}

req.session.destroy(() => {
    res.status(205).send();
});
             **/

            return CreatedAtAction(nameof(Post), new { id = message.Address }, result);
        }
    }
}
