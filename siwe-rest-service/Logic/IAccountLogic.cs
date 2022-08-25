using Nethereum.Siwe.Core;

using siwe.Messages;

namespace siwe_rest_service.Logic
{
    public interface ITokenLogic
    {
        public string GetAuthenticationToken(MySiweMessage loginMsg);
    }
}
