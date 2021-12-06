using siwe.Messages;

namespace siwe_rest_service.Models
{
    public class SiweSession
    {
        public SiweMessage? siwe { get; set; }

        public string? nonce { get; set; }

        public string? ens { get; set; }
    }
}
