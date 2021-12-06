using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
            SiweMeResult result =
                new SiweMeResult() { Address = message.Address, Text = message.ToMessage(), Ens = String.Empty };

            string? nonce = TempData.ContainsKey("nonce") ? (string) TempData["nonce"] : String.Empty;

            if (nonce != message.Nonce)
                return BadRequest();

            HttpContext.Session.SetString("siwe",  message.ToString());
            HttpContext.Session.SetString("ens",   string.Empty);
            HttpContext.Session.SetString("nonce", string.Empty);

            /**
             ** NOTE: To be ported
             **
app.post('/api/sign_in', async (req, res) => {
    try {
        const { ens } = req.body;
        if (!req.body.message) {
            res.status(422).json({ message: 'Expected signMessage object as body.' });
            return;
        }

        const message = new SiweMessage(req.body.message);

        const infuraProvider = new providers.JsonRpcProvider(
            {
                allowGzip: true,
                url: `${getInfuraUrl(message.chainId)}/8fcacee838e04f31b6ec145eb98879c8`,
                headers: {
//                    Accept: '**',
                    Origin: `http://localhost:${PORT}`,
                    'Accept-Encoding': 'gzip, deflate, br',
                    'Content-Type': 'application/json',
                },
            },
            Number.parseInt(message.chainId),
        );

await infuraProvider.ready;

const fields: SiweMessage = await message.validate(infuraProvider);

if (fields.nonce !== req.session.nonce)
{
    res.status(422).json({
    message: `Invalid nonce.`,
            });
    return;
}

req.session.siwe = fields;
req.session.ens = ens;
req.session.nonce = null;
req.session.cookie.expires = new Date(fields.expirationTime);
req.session.save(() =>
    res
        .status(200)
        .json({
text: getText(req.session.siwe.address),
                    address: req.session.siwe.address,
                    ens: req.session.ens,
                })
                .end(),
        );
    } catch (e)
{
    req.session.siwe = null;
    req.session.nonce = null;
    req.session.ens = null;
    console.error(e);
    switch (e)
    {
        case ErrorTypes.EXPIRED_MESSAGE:
            {
                req.session.save(() => res.status(440).json({ message: e.message }));
                break;
            }
        case ErrorTypes.INVALID_SIGNATURE:
            {
                req.session.save(() => res.status(422).json({ message: e.message }));
                break;
            }
        default:
            {
                req.session.save(() => res.status(500).json({ message: e.message }));
                break;
            }
    }
}
});
            **/

            return CreatedAtAction(nameof(Post), new { id = message.Address }, result);
        }
    }
}
