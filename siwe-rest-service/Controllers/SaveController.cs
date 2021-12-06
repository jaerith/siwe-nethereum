using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

using siwe.Messages;

namespace siwe_rest_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaveController : Controller
    {
        // PUT api/<SaveController>/5
        [HttpPut]
        public IActionResult Update([FromBody] SiweMessage message)
        {
            if ((message == null) || string.IsNullOrEmpty(message.Address))
                return BadRequest();

            /**
             ** NOTE: To be ported
             **
app.put('/api/save', async (req, res) => {
if (!req.session.siwe)
{
    res.status(401).json({ message: 'You have to first sign_in' });
return;
    }

    await fs.readdir(Path.resolve(__dirname, `.. / db`), (err, files) => {
    if (files.length === 1000001)
    {
        res.status(500).json({ message: 'File limit reached!' });
return;
        }
    });

updateText(req.body.text, req.session.siwe.address);
res.status(204).send().end();
});
          **/

            return NoContent();
        }
    }
}
