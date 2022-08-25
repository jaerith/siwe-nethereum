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

        public Task<MySiweMessageAndText> GetNotepadText(string address);

        public Task<MySiweMessageAndText> PostSignIn(MySiweMessage message);

        public Task<SiweMeResult> PostSignOut(MySiweMessage message);

        public Task<MySiweMessageAndText> PutSave(MySiweMessageAndText message);
    }
}
