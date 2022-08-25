using Nethereum.Siwe.Core;

using siwe.Messages;

namespace siwe_rest_service.Models
{
    public class SiweSession
    {
        public MySiweMessage? siwe { get; set; }

        public string? nonce { get; set; }

        public string? ens { get; set; }
    }
}
