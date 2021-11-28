using System;
using System.Threading.Tasks;

using siwe.Messages;

namespace siwe_nethereum.Data
{
    /**
     ** NOTE: Perhaps this class will be a simple proxy, where these methods then call an actual REST service?
     **/
    public class SiweService
    {
        public Task<SiweMeResult> GetMe(SiweMessage message)
        {
            /**
             ** NOTE: To be ported
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

            return Task.FromResult(new SiweMeResult());
        }

        public Task<string> GetNonce()
        {
            /**
             ** NOTE: To be ported
                /*
                req.session.nonce = (Math.random() + 1).toString(36).substring(4);
                req.session.save(() => res.status(200).send(req.session.nonce).end());
              **/

            return Task.FromResult(SiweMessage.GetNonce());
        }

        public Task<SiweMeResult> PostSignIn(SiweMessage message)
        {
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

            return Task.FromResult(new SiweMeResult());
        }

        public Task<string> PostSignOut(SiweMessage message)
        {
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

            return Task.FromResult(String.Empty);
        }

        public Task<string> PutSave(SiweMessage message, string text)
        {
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

            return Task.FromResult(String.Empty);
        }

    }
}
