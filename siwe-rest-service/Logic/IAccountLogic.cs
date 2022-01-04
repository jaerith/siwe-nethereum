using Nethereum.Siwe.Core;

namespace siwe_rest_service.Logic
{
    public interface ITokenLogic
    {
        public string GetAuthenticationToken(SiweMessage loginMsg);
    }
}
