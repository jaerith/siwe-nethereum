using System.Threading.Tasks;

using siwe.Messages;
using siwe_rest_service.Models;

namespace siwe_nethereum.RestServices
{
    public interface ISignDocService
    {
        public Task<DocSignData> GetSignatureDateTime(MySiweMessage message);

        public Task<DocSignData> PostDocumentRegistration(MySiweMessage message);

        public Task<DocSignData> PutDocumentSignature(MySiweMessage message);
    }
}
