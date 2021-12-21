using System.Threading.Tasks;

using Nethereum.Siwe.Core;

using siwe.Messages;
using siwe_rest_service.Models;

namespace siwe_nethereum.RestServices
{
    public interface ISiweRestService
    {
        public Task<SiweMeResult> GetMe();

        public Task<string> GetNonce();

        public Task<SiweMessageAndText> PostSignIn(SiweMessage message);

        public Task<SiweMeResult> PostSignOut(SiweMessage message);

        public Task<SiweMessageAndText> PutSave(SiweMessageAndText message);
    }
}
